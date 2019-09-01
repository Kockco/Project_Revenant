using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRotation : MonoBehaviour
{
    enum STATE
    {
        stop,
        move
    }
    STATE state;

    public CrystalState c_state;
    public Transform parent;

    public float RotY;
    [Range(0, -179)]
    public int minLimit;
    [Range(0, 179)]
    public int maxLimit;
    [Range(0, 30)]
    public int stopPointDistance;

    public int myPoint;
    int stopPointCount = 0;
    public int[] stopPoint;
    public float movingTime;
    float myTime;

    private void Start()
    {
        RotY = 0;
        state = STATE.stop;
        c_state = GetComponent<CrystalState>();
        c_state.state = C_STATE.EMPTY;

        parent = transform.parent;
        movingTime = 1;
        myTime = 0;

        //왼쪽 오른쪽 최대 지점을 정하고 구간계산
        if ((maxLimit + (-minLimit)) % stopPointDistance == 0)
        {
            stopPointCount = ((maxLimit + (-minLimit)) / stopPointDistance) + 1; //0때문에 1더함
            stopPoint = new int[stopPointCount];
            int section = (maxLimit + (-minLimit))/ (stopPointCount-1);
            for (int i = 0; i < stopPointCount; i++)
            {
                stopPoint[i] = (section * i) + minLimit;
                if(stopPoint[i] == 0)
                {
                    myPoint = i;
                }
            }
            
        }
        else
        {
            Debug.Log("Stop Point Error");
            stopPoint = null;
        }

        if (transform.parent == null)
            Debug.Log("parent not find");

        if (minLimit >= maxLimit)
            Debug.Log("Limit Error");
    }
    private void Update()
    {
        StairChange();
        if(parent.rotation.eulerAngles.y > 180)
        {
            RotY = parent.rotation.eulerAngles.y-360;
        }
        else
        {
            RotY = parent.rotation.eulerAngles.y;
        }
    }

    void StairChange()
    {
        switch (c_state.state)
        {
            case C_STATE.EMPTY:
                if(RotY > -1)
                {
                    parent.transform.Rotate(Vector3.up * Time.deltaTime * 10);
                }
                else if(RotY < 1)
                {
                    parent.transform.Rotate(Vector3.down * Time.deltaTime * 10);
                }
                else
                {
                    myPoint = 3;
                }
                break;
            case C_STATE.BLUE:
                if (myPoint < stopPointCount-1) // 일단 끝이면 아무것도안함
                {
                    if (state == STATE.move) //움직여야할때는
                    {
                        if (stopPoint[myPoint + 1] > RotY) //자기보다 한칸 위쪽포지션까지이동
                        {
                            Debug.Log("일반"+stopPoint[myPoint + 1]);
                            parent.transform.Rotate(Vector3.up * Time.deltaTime*10);
                        }
                        else
                        {
                            Debug.Log("변경됨");
                            myPoint += 1;
                            state = STATE.stop;
                        }
                    }
                    else
                    {
                        myTime += Time.deltaTime;
                        if (myTime > movingTime)
                        {
                            state = STATE.move;
                            myTime = 0;
                        }
                    }
                }
                break;
            case C_STATE.WHITE:
                if (myPoint > 0) // 일단 끝이면 아무것도안함
                {
                    if (state == STATE.move) //움직여야할때는
                    {
                        if (stopPoint[myPoint - 1] < RotY)
                        {
                            Debug.Log(RotY);
                            parent.transform.Rotate(Vector3.down * Time.deltaTime * 10);
                        }
                        else
                        {
                            myPoint -= 1;
                            state = STATE.stop;
                        }
                    }
                    else
                    {
                        myTime += Time.deltaTime;
                        if (myTime > movingTime)
                        {
                            state = STATE.move;
                            myTime = 0;
                        }
                    }
                }
                break;
            case C_STATE.RED:
                break;
            case C_STATE.BLACK:
                break;
        }
    }
}
