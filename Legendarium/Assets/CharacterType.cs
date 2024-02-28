using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterInfo", menuName = "ScriptableObjects/CharacterInfo", order = 1)]
public class CharacterType : ScriptableObject
{
    public Animator baseAnimator;
    [Tooltip("The amount of health this player has")]
    public int maxHealth = 1000;
    [Tooltip("Mana allows the player to cast spells")]
    public int maxMana = 500;
    [Tooltip("Base Melee attack bonus percent")]
    public float baseMeleeDamage = 1;
    [Tooltip("Base Magic attack bonus percent")]
    public float baseMagicDamage = 1;
    [Tooltip("Base movement speed percent")]
    public int startingSpeed = 10;
}
