using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState
{
    private Player player;
    void IState.OnEnter(Player player)
    {
        //player 프로퍼티 초기화
        this.player = player;
        // 초기화 구현
    }
    void IState.Update()
    {
        // 실행할것 구현
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.SetState(new PlayerIdleState());
        }
    }
    void IState.OnExit()
    {
        //종료되면서 정리해야할것 구현
    }
}
