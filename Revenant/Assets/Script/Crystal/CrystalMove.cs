using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMove : MonoBehaviour
{
    public GameObject[] pos;
    public GameObject target;
    public CrystalState c_state;
    private void Start()
    {
        c_state = GetComponent<CrystalState>();
        c_state.state = C_STATE.EMPTY;
    }
    private void Update()
    {
        //if (transform.position == pos[4].transform.position)
        //{
        //    c_state.state = C_STATE.EMPTY;
        //}
        //중앙이면 타겟으로 위치 변경!
        if (transform.position == pos[4].transform.position && c_state.state != C_STATE.EMPTY)
        {
            TargetPosChange();
        }

        if (c_state.changeMat == true || c_state.state == C_STATE.EMPTY)
        {
            target = pos[4];
        }

        //중앙이 아닐때는 중앙으로 /중앙 포지션과는 비슷해지면 초기화
        if (((target.transform.position.z - transform.position.z) < 0.1f && (target.transform.position.z - transform.position.z) > -0.1f)
            && ((target.transform.position.x - transform.position.x) < 0.1f && (target.transform.position.x - transform.position.x) > -0.1f))
        {
            transform.position = target.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //움직임
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.fixedDeltaTime * 2f);
        
    }

    void TargetPosChange()
    {
        switch (c_state.state)
        {
            case C_STATE.EMPTY:
                break;
            case C_STATE.BLUE:
                target = pos[0]; //empty
                break;
            case C_STATE.WHITE:
                target = pos[1]; //empty
                break;
            case C_STATE.RED:
                target = pos[2]; //empty
                break;
            case C_STATE.BLACK:
                target = pos[3]; //empty
                break;
        }
    }
}