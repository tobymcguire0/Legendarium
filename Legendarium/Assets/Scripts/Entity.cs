using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EntityData
{
    public float currentHealth;
    public float maxHealth;
    public float currentMana;
    public float maxMana;
    public float score;
}
[RequireComponent(typeof(Collider2D),typeof(Animator))]
public abstract class Entity : MonoBehaviour
{
    protected EntityData entityData;
    public virtual void Damage(int amount)
    {
        entityData.currentHealth-=amount;
        if(entityData.currentHealth <= 0 ) 
        {
            entityData.currentHealth = 0;
            Die();
        }
    }
    protected abstract void Die();
    public virtual void Heal(int amount)
    {
        entityData.currentHealth+=amount;
        if(entityData.currentHealth > entityData.maxHealth) entityData.currentHealth=entityData.maxHealth;
    }
}
