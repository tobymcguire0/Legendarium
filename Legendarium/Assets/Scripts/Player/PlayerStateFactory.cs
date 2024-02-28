using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerController psm;
    public PlayerStateFactory(PlayerController psm)
    {
        this.psm = psm;
    }

    public PlayerState Idle()
    {
        return new PlayerIdle(this,psm);
    }
    public PlayerState Move()
    {
        return new PlayerMove(this, psm);
    }
    public PlayerState Melee()
    {
        return new PlayerMelee(this, psm);
    }
    public PlayerState Magic()
    {
        return new PlayerMagic(this, psm);
    }
}
