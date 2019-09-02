using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerState currentState;

    public Vector3 move;
    public Transform myTransform;
    public float runSpeed = 5;
    public float gravity = 9.81f;
    public float yVelocity = 0;
    public float jumpPower = 10f;

    public Transform model;
    public Transform cameraTransform;
    public GameObject staff;
    public Transform aim;

    CharacterController cc;
    private void Awake()
    {
        //Start idleState
        SetState(new PlayerIdleState());

        staff = GameObject.Find("Staff");
        if (staff == null)
            Debug.Log("PlayerScript Error : staff not find");

        cc = GetComponent<CharacterController>();
        model = transform.GetChild(0);
        cameraTransform = Camera.main.transform.parent;
        myTransform = transform;
    }

    private void Update()
    {
        //state Update
        currentState.Update();

        cc.Move(move * Time.deltaTime);
    }

    public void SetState(PlayerState nextState)
    {
        //other state change
        if (currentState != null)
        {
            currentState.OnExit();
        }

        // next state start
        currentState = nextState;
        currentState.OnEnter(this);
    }
    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.position.y < transform.position.y)
        {
            //Debug.Log("충돌됨");
            yVelocity = 0;
        }
    }
    public void MoveCalc(float ratio)
    {
        float tempMoveY = move.y;
        move.y = 0; //이동에는 xz값만 필요하므로 잠시 저장하고 빼둔다.
        Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //대각선 이동이 루트2 배의 속도를 갖는 것을 막기위해 속도가 1 이상 된다면 노말라이즈 후 속도를 곱해 어느 방향이든 항상 일정한 속도가 되게 한다.
        float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude; //sqrMagnitude연산을 두 번 할 필요 없도록 따로 저장
        inputMoveXZ = myTransform.TransformDirection(inputMoveXZ);
        if (inputMoveXZMgnitude <= 1)
            inputMoveXZ *= runSpeed;
        else
            inputMoveXZ = inputMoveXZ.normalized * runSpeed;

        //조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록 한다.
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraTransform.rotation;
            cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.
            //자연스러움을 위해 Slerp로 회전시킨다.
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, cameraRotation, 10.0f * Time.deltaTime);
            if (move != Vector3.zero)//Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                model.rotation = Quaternion.Slerp(model.rotation, characterRotation, 10.0f * Time.deltaTime);
            }

            //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
            move = Vector3.MoveTowards(move, inputMoveXZ, ratio * runSpeed);
        }
        else
        {
            //조작이 없으면 서서히 멈춘다.
            move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * runSpeed * ratio);
        }
        float speed = move.sqrMagnitude;    //현재 속도를 애니메이터에 세팅한다.

        move.y = tempMoveY; //y값 복구
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SetState(new PlayerAirborne());
            yVelocity = jumpPower;
        }
        move.y = yVelocity;
        if (yVelocity > -19)
            yVelocity -= gravity * 3 * Time.deltaTime;
    }
    
    public void PlayerAnimation(string aniName) { model.GetComponent<Animator>().SetTrigger(aniName); }

    //스태프의 getset
    public C_STATE GetStaffState() { return staff.GetComponent<PlayerStaff>().GetState();}
    public int GetStaffCryNumber() { return staff.GetComponent<PlayerStaff>().GetCrystalNum(); }
    public void ChangeStaffNum(int num) { staff.GetComponent<PlayerStaff>().ChangeNum(num); }
    public void ChangeStaffMaterial() { staff.GetComponent<PlayerStaff>().ChangeMaterial(); }
    public void ChangeStaffMaterial(Material mat) { staff.GetComponent<PlayerStaff>().ChangeMaterial(mat); }
    public void ChangeStaffState(C_STATE state) { staff.GetComponent<PlayerStaff>().ChangeState(state); }
    
}