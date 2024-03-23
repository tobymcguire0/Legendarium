using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerStateFactory factory, PlayerController psm) : base(factory, psm)
    {

    }
    public override void CheckSwitchStates()
    {
        if (player.IsMeleePressed)
        {
            SwitchState(factory.Melee());
            return;
        }
        if (player.IsMagicPressed)
        {
            SwitchState(factory.Magic());
            return;
        }
        if (player.MovingDirection != Vector2.zero)
        {
            SwitchState(factory.Move());
        }
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        //Idle
    }

    public override void Update(float deltaTime)
    {
        //Idle
        CheckSwitchStates();
    }
}
