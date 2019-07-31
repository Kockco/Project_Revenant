using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private GameObject target;
    public Material mat;
    public Material normalMat;

    private void Start()
    {
        mat = normalMat;
    }

    void Update()
    {
        GetComponent<MeshRenderer>().material = mat;
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if (target.name == "Empty_Crystal")
            {
                target.GetComponent<MeshRenderer>().material = mat;
            }
            else if (target.tag == "Crystal")
            {
                mat = target.GetComponent<CrystalState>().mat;
            }

            Debug.Log(target.name);


            if (target.Equals(gameObject))  //선택된게 나라면
            {
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            mat = normalMat;
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
