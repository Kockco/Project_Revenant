using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public IState currentState;
    public bool moveKeyState;
    public float speed = 10;
    new Rigidbody rigidbody;
    Vector3 movement;
    public float h, v;

    private void Awake()
    {
        //Start idleState
        SetState(new PlayerIdleState());
        rigidbody = GetComponent<Rigidbody>();
        moveKeyState = false;
    }

    private void Update()
    {
        //state Update
        currentState.Update();
        moveKeyState = MoveKeyState();
        CharacterMove();
        Debug.Log(moveKeyState);
    }

    public void SetState(IState nextState)
    {
        //other state change
        if (currentState != null)
        {
            currentState.OnExit();
        }

        // next state start
        currentState = nextState;
        currentState.OnEnter(this);
    }

    public void CharacterMove()
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        Debug.Log(movement);
        rigidbody.MovePosition(transform.position + movement);
    }

    public bool MoveKeyState()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (h == 0 && v == 0)
        {
            return false;
        }
        else
            return true;
    }
}