using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : MonoBehaviour
{
    public GameObject col;
    public bool isCol;

    private void Start()
    {
        col = null;
        isCol = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Crystal" || other.gameObject.tag == "Empty_Crystal" )
        {
            col = other.gameObject;
            isCol = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Crystal" || other.gameObject.tag == "Empty_Crystal")
        {
            col = null;
            isCol = false;
        }
    }
}
