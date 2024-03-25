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
    protected float invincibilityTimer;
    public virtual void Damage(int amount, Vector2 knockback)
    {
        if (invincibilityTimer > 0) return;
        Debug.Log(name + " damaged for " + amount);
        entityData.currentHealth-=amount;
        invincibilityTimer = .2f;
        if(entityData.currentHealth <= 0 ) 
        {
            entityData.currentHealth = 0;
            Die();
        }
    }
    public virtual void Update()
    {
        if (invincibilityTimer > 0) invincibilityTimer -= Time.deltaTime;
    }
    protected abstract void Die();
    public virtual void Heal(int amount)
    {
        entityData.currentHealth+=amount;
        if(entityData.currentHealth > entityData.maxHealth) entityData.currentHealth=entityData.maxHealth;
    }

    
}
