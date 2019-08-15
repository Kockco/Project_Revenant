using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotation : MonoBehaviour
{
    public CrystalState c_state;
    private void Start()
    {
        c_state = GetComponent<CrystalState>();
        c_state.state = C_STATE.EMPTY;
    }
    private void Update()
    {
        StairChange();
    }

    void StairChange()
    {
        switch (c_state.state)
        {
            case C_STATE.EMPTY:
                break;
            case C_STATE.BLUE:
                transform.parent.transform.Rotate(Vector3.up * Time.deltaTime * 15);
                break;
            case C_STATE.WHITE:
                transform.parent.transform.Rotate(Vector3.down * Time.deltaTime * 15);
                break;
            case C_STATE.RED:
                break;
            case C_STATE.BLACK:
                break;
        }
    }
}
