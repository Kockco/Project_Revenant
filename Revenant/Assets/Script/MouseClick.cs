using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private GameObject target;
    public Material mat;
    public Material normalMat;
     C_STATE state;

    private void Start()
    {
        mat = normalMat;
        state = C_STATE.EMPTY;
    }

    void Update()
    {
        GetComponent<MeshRenderer>().material = mat;

        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if (target.name == "Empty_Crystal")
            {
                //크리스탈이 달라야만 바꿔준다.
                if (target.GetComponent<CrystalState>().state != state)
                {
                    target.GetComponent<CrystalState>().state = state;
                    target.GetComponent<CrystalState>().changeMat = true;
                }
            }
            else if (target.tag == "Crystal")
            {
                mat = target.GetComponent<CrystalState>().myMat.material;
                state = target.GetComponent<CrystalState>().state;
            }

            Debug.Log(target.name);


            if (target.Equals(gameObject))  //선택된게 나라면
            {
            }
        }
        //크리스탈을 무색으로 초기화
        if (Input.GetMouseButtonDown(1))
        {
            mat = normalMat;
            state = C_STATE.EMPTY;
        }
    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;
        }
        return target;

    }
    
}
