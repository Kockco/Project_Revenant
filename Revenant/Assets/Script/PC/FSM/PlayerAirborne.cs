using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborne : PlayerState
{
    private Player player;
    void PlayerState.OnEnter(Player player) { }

    void PlayerState.Update() {
        Debug.Log("에어본");
    }

        void PlayerState.OnExit() { }

}
