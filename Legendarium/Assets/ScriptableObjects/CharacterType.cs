using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterInfo", menuName = "ScriptableObjects/CharacterInfo", order = 1)]
public class CharacterType : ScriptableObject
{
    [Tooltip("The amount of health this player has")]
    public int maxHealth = 1000;
    [Tooltip("Mana allows the player to cast spells")]
    public int maxMana = 500;
    [Tooltip("Base Melee attack bonus percent")]
    [Range(0.2f,2)]
    public float baseMeleeDamage = 1;
    [Tooltip("Base Magic attack bonus percent")]
    [Range(0.2f,2)]
    public float baseMagicDamage = 1;
    [Tooltip("Base movement speed percent")]
    [Range(0.2f,2)]
    public float startingSpeed = 1;
}
