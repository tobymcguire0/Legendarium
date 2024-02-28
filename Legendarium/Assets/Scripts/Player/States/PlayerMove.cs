using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    public PlayerMove(PlayerStateFactory factory, PlayerStateMachine psm) : base(factory, psm)
    {

    }
    public override void CheckSwitchStates()
    {
        //Do sth at some point
    }

    public override void EnterState()
    {
        //Idle
    }

    public override void ExitState()
    {
        //Idle
    }

    public override void Update()
    {
        //Idle
    }
}
