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

    }

    public override void ExitState()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void Update(float deltaTime)
    {
        if(player.MovingDirection!= Vector2.zero) 
            player.FacingDirection = player.MovingDirection;
        Vector2 nextPosition = player.MovingDirection * player.MoveSpeed * player.CharacterData.startingSpeed*deltaTime;

        player.KinematicMove(nextPosition);
        CheckSwitchStates();
    }
}
