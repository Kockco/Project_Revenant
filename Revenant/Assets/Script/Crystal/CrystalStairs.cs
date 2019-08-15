using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalStairs : MonoBehaviour
{
    public GameObject[] stairs;
    public CrystalState c_state;
    private void Start()
    {
        c_state = GetComponent<CrystalState>();
        c_state.state = C_STATE.EMPTY;
        stairs = new GameObject[4];
        for (int i = 0; i < 4; i++)
            stairs[i] = transform.GetChild(i).gameObject;
    }
    private void Update()
    {
        StairChange();
    }

    void StairChange()
    {
        for (int i = 0; i < 4; i++)
        {
            if (stairs[i].activeSelf == true)
                stairs[i].SetActive(false);
        }
        switch (c_state.state)
        {
            case C_STATE.EMPTY:
                break;
            case C_STATE.BLUE:
                stairs[0].SetActive(true);
                break;
            case C_STATE.WHITE:
                stairs[1].SetActive(true);
                break;
            case C_STATE.RED:

                stairs[2].SetActive(true);
                break;
            case C_STATE.BLACK:
                stairs[3].SetActive(true);
                break;
        }
    }
}
