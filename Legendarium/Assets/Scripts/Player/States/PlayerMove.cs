using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerState
{
    public PlayerMove(PlayerStateFactory factory, PlayerController psm) : base(factory, psm)
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
        if (player.MovingDirection == Vector2.zero)
        {
            SwitchState(factory.Idle());
        }
    }

    public override void EnterState()
    {
        Debug.Log("Entered Move State");

    }

    public override void ExitState()
    {
        //Idle
    }

    public override void Update()
    {
        player.transform.position += (Vector3)(player.MovingDirection *player.MoveSpeed* player.CharacterData.startingSpeed * Time.deltaTime);
        if(player.MovingDirection!= Vector2.zero) 
            player.FacingDirection = player.MovingDirection;

        CheckSwitchStates();
    }
}
