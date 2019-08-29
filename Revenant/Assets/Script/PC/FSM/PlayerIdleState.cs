using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
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
        player.MoveCalc(1.0f);
        player.move.y -= player.gravity * Time.deltaTime;
        player.Jump();
        //GradientCheck();
    }
    void PlayerState.OnExit()
    {
        //종료되면서 정리해야할것 구현
    }
    
    void GradientCheck()
    {
        if (Physics.Raycast(player.myTransform.position, Vector3.down, 0.2f))
        //경사로를 구분하기 위해 밑으로 레이를 쏘아 땅을 확인한다.
        //CharacterController는 밑으로 지속적으로 Move가 일어나야 땅을 체크하는데 -y값이 너무 낮으면 조금만 경사져도 공중에 떠버리고 너무 높으면 절벽에서 떨어질때 추락하듯 바로 떨어진다.
        //완벽하진 않지만 캡슐 모양의 CharacterController에서 절벽에 떨어지기 직전엔 중앙에서 밑으로 쏘아지는 레이에 아무것도 닿지 않으므로 그때만 -y값을 낮추면 경사로에도 잘 다니고
        //절벽에도 자연스럽게 천천히 떨어지는 효과를 줄 수 있다.
        {
            player.move.y = -5;
        }
        else
            player.move.y = -1;
    }
    

}

