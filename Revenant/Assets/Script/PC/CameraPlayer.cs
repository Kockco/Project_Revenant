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
    

    // Use this for initialization
    void Awake()
    {
        topView = false;
        myTransform = transform;
        model = transform.GetChild(0);
        cameraParentTransform = Camera.main.transform.parent;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        cameraParentTransform.position = myTransform.position + Vector3.up * 1.4f;  //캐릭터의 머리 높이쯤

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
        if (-1 < Camera.main.transform.localPosition.z)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -1);    //최대로 가까운 수치
        else if (Camera.main.transform.localPosition.z < -5)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -5);    //최대로 먼 수치
    }
    
}
