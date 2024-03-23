using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : PlayerState
{
    public PlayerMagic(PlayerStateFactory factory, PlayerController psm) : base(factory, psm)
    {

    }
    public override void CheckSwitchStates()
    {
        if (attackTimer > 0) return;
        if (player.MovingDirection != Vector2.zero)
        {
            SwitchState(factory.Move());
            return;
        }
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
            return;
        }
    }

    public override void EnterState()
    {
        player.FireProjectile();

        attackTimer = .4f;
    }

    public override void ExitState()
    {
    }
    float attackTimer;
    public override void Update(float deltaTime)
    {
        attackTimer -= deltaTime;
        CheckSwitchStates();
    }
}
