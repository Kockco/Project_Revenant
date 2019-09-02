using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    int crystalNum;
    Material mat;
    public Material normalMat;
    C_STATE state;

    private void Start()
    {
        mat = normalMat;
        state = C_STATE.EMPTY;
        crystalNum = 0;
    }
    
    void Update()
    {
    }

    public void ChangeMaterial()
    {
        GetComponent<MeshRenderer>().material = normalMat;
    }
    public void ChangeMaterial(Material changeMat)
    {
        mat = changeMat;
        GetComponent<MeshRenderer>().material = mat;
    }
    public void ChangeState(C_STATE changeState)
    {
        state = changeState;
    }

    public void ChangeNum(int changeNum) { crystalNum = changeNum; }
    public int GetCrystalNum() { return crystalNum; }
    public C_STATE GetState() { return state; }
    public Material GetMaterial() { return mat; }
    
}
