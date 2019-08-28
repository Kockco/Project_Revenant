using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private Player player;
    void PlayerState.OnEnter(Player player)
    {
        //player 프로퍼티 초기화
        this.player = player;
        // 초기화 구현
    }
    void PlayerState.Update()
    {
        // 실행할것 구현
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.SetState(new PlayerIdleState());
        }
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
    }
}
