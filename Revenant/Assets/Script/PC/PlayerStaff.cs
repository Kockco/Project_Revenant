using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaff : MonoBehaviour
{
    public Material mat;
    public Material normalMat;
    public C_STATE state;

    private void Start()
    {
        mat = normalMat;
        state = C_STATE.EMPTY;
    }
    
    void Update()
    {
        GetComponent<MeshRenderer>().material = mat;
    }
}
