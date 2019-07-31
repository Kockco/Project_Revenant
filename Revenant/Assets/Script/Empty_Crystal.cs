using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE { RED, YELLOW, BLUE, GREEN, CENTER }

public class Empty_Crystal : MonoBehaviour
{
    public GameObject[] pos;
    public GameObject target;
    public STATE state;
    private void Start()
    {
        state = STATE.CENTER;
    }
    private void Update()
    {
        TargetPosChange();
        
        if (transform.position == pos[4].transform.position)
        {
            state = STATE.CENTER;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.fixedDeltaTime * 2f);
        if (state == STATE.BLUE || state == STATE.GREEN)
        {
            if ((target.transform.position.z - transform.position.z) < 0.1f && (target.transform.position.z - transform.position.z) > -0.1f)
            {
                transform.position = target.transform.position;
            }
        }
        else if (state == STATE.RED || state == STATE.YELLOW)
        {
            if ((target.transform.position.x - transform.position.x) < 0.1f && (target.transform.position.x - transform.position.x) > -0.1f)
            {
                transform.position = target.transform.position;
            }
        }
    }

    void TargetPosChange()
    {
        switch(state)
        {
            case STATE.CENTER:
                if (GetComponent<MeshRenderer>().material.name == "Crystal1 (Instance) (Instance)")
                {
                    target = pos[0]; //red
                    state = STATE.RED;
                }
                else if (GetComponent<MeshRenderer>().material.name == "Crystal2 (Instance) (Instance)")
                {
                    target = pos[1]; //yellow
                    state = STATE.YELLOW;
                }
                else if (GetComponent<MeshRenderer>().material.name == "Crystal3 (Instance) (Instance)")
                {
                    target = pos[2]; //blue
                    state = STATE.BLUE;
                }
                else if (GetComponent<MeshRenderer>().material.name == "Crystal4 (Instance) (Instance)")
                {
                    target = pos[3]; //green
                    state = STATE.GREEN;
                }
                else if (GetComponent<MeshRenderer>().material.name == "Empty (Instance)")
                {
                    target = pos[4]; //empty
                }
                break;
            case STATE.RED:
                if (GetComponent<MeshRenderer>().material.name != "Crystal1 (Instance) (Instance)")
                {
                    target = pos[4]; //empty
                }
                break;
            case STATE.YELLOW:
                if (GetComponent<MeshRenderer>().material.name != "Crystal2 (Instance) (Instance)")
                {
                    target = pos[4]; //empty
                }
                break;
            case STATE.BLUE:
                if (GetComponent<MeshRenderer>().material.name != "Crystal3 (Instance) (Instance)")
                {
                    target = pos[4]; //empty
                }
                break;
            case STATE.GREEN:
                if (GetComponent<MeshRenderer>().material.name != "Crystal4 (Instance) (Instance)")
                {
                    target = pos[4]; //empty
                }
                break;

        }
    }
}
