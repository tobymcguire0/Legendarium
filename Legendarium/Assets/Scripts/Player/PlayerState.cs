using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateFactory factory;
    protected PlayerController player;
    public PlayerState(PlayerStateFactory factory, PlayerController psm)
    {
            player = psm;
            this.factory = factory;
    }
    public abstract void CheckSwitchStates();
    public abstract void EnterState();
    public abstract void ExitState(); 
    protected void SwitchState(PlayerState newState)
    {
        ExitState();
        newState.EnterState();
        player.CurrentState = newState;
    }
    public abstract void Update();
}
