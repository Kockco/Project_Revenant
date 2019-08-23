using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum C_STATE { BLUE, WHITE, RED, BLACK, EMPTY }

public class CrystalState : MonoBehaviour
{
    public C_STATE state;
    public Material[] mat;
    public MeshRenderer myMat;

    public bool changeMat;
    float timeCheck;
    void Start()
    {
        //마테리얼 미리 리소스 로드
        mat = new Material[5];
        mat[0] = Resources.Load<Material>("Test/Crystal1");
        mat[1] = Resources.Load("Test/Crystal2", typeof(Material))as Material;
        mat[2] = Resources.Load("Test/Crystal3", typeof(Material)) as Material;
        mat[3] = Resources.Load("Test/Crystal4", typeof(Material)) as Material;
        mat[4] = Resources.Load("Test/Empty", typeof(Material)) as Material;
        myMat = GetComponent<MeshRenderer>();
        changeMat = false;
        timeCheck = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMat();
        //마테리얼이 바뀌었으면 바뀌었다는 신호를 시간만큼 줍니다.
        if(changeMat == true)
        {
            timeCheck += Time.deltaTime;
            if(timeCheck >=0.5f)
            {
                timeCheck = 0;
                changeMat = false;
            }
        }
    }

    void ChangeMat()
    {
        switch (state)
        {
            case C_STATE.BLUE:
                myMat.material = mat[0];
                break;
            case C_STATE.WHITE:
                myMat.material = mat[1];
                break;
            case C_STATE.RED:
                myMat.material = mat[2];
                break;
            case C_STATE.BLACK:
                myMat.material = mat[3];
                break;
            case C_STATE.EMPTY:
                myMat.material = mat[4];
                break;
        }
    }

}
