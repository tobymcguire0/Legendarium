using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    PlayerController player;
    PlayerStateFactory states;
    PlayerState currentState;
    CharacterType characterData;
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterType CharacterData { get { return characterData; } }
    public PlayerStateMachine(PlayerController player, CharacterType data)
    {
        this.player = player;
        characterData = data;
        states = new PlayerStateFactory(this);
    }

    public void Update()
    {
        currentState?.Update();
    }
}
