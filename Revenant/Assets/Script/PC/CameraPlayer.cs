using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 2.0f;  //카메라 마우스 감도
    
    Vector3 dir;

    Transform myTransform;
    Transform model;

    Vector3 mouseMove;
    Transform cameraParentTransform;

    bool topView;

    [Header("캐릭터 크기 조정")]
    [Range(0, 5)]
    public float charSize =1 ;
    [Header("카메라 높이 조정")]
    [Range(0, 5)]
    public float  camHeight = 1.4f;
    [Header("시작하는 카메라 거리")]
    [Range(0,-30)]
    public float startDistance = -4.1f;
    [Header ("현재 거리(수정불가)")]
    [SerializeField]
    float nowDistance;

    // Use this for initialization
    void Awake()
    {
        topView = false;
        myTransform = transform;
        model = transform.GetChild(0);
        cameraParentTransform = Camera.main.transform.parent;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, startDistance);
    }

    // Update is called once per frame
    void Update()
    {
        Balance();
        CameraDistanceCtrl();

        if (Input.GetKeyDown(KeyCode.T)) // 탑뷰로 바꾸기
        {
            if (!topView)
                topView = true;
            else
                topView = false;
        }
        //test 용
        nowDistance = Camera.main.transform.localPosition.z;
        transform.localScale = new Vector3(charSize, charSize, charSize);


    }

    void LateUpdate()
    {
        if(!topView)
            MouseSense();
        else
            TopView();
        //MouseSense();
       // MyView();
    }

    void MouseSense()
    {
        cameraParentTransform.position = myTransform.position + Vector3.up * camHeight;  //캐릭터의 머리 높이쯤

        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity, Input.GetAxisRaw("Mouse X") * mouseSensitivity, 0);   //마우스의 움직임을 가감
        if (mouseMove.x < -40)  //위로 볼수있는 것 제한 90이면 아예 땅바닥에서 하늘보기
            mouseMove.x = -40;
        else if (30 < mouseMove.x) //위에서 아래로 보는것 제한 
            mouseMove.x = 30;

        cameraParentTransform.localEulerAngles = mouseMove;
    }

    void TopView()
    {
        cameraParentTransform.position = myTransform.position + Vector3.up * 25; //캐릭터 머리 훨씬위
        cameraParentTransform.localEulerAngles = new Vector3(90, 0 ,0);
    }

    void MyView()
    {
        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, 0);
        cameraParentTransform.position = myTransform.position + Vector3.up; //캐릭터 머리 훨씬위
        //cameraParentTransform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void Balance()
    {
        if (myTransform.eulerAngles.x != 0 || myTransform.eulerAngles.z != 0)   //대각선으로 틀어질 경우는 없어야하니 바로잡기
            myTransform.eulerAngles = new Vector3(0, myTransform.eulerAngles.y, 0);
    }

    void CameraDistanceCtrl()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f); //휠로 카메라의 거리를 조절한다.
        if (0 < Camera.main.transform.localPosition.z) //  -1 이 가장 나음?
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, 0);    //최대로 가까운 수치
        else if (Camera.main.transform.localPosition.z < -30) // - 5까지였음
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -30);    //최대로 먼 수치
    }
    
}
