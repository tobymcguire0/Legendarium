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
[RequireComponent(typeof(Collider2D))]
public abstract class Entity : MonoBehaviour
{
    [Header("Entity Stuff")]
    [SerializeField] GameObject deathExplosion;
    [SerializeField] ParticleSystem HurtParticles;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject damagePopup;
    protected EntityData entityData;
    protected float invincibilityTimer;
    public virtual void Damage(int amount, Vector2 knockback)
    {
        if (invincibilityTimer > 0) return;
        Debug.Log(name + " damaged for " + amount);
        if (HurtParticles != null)
        {
            HurtParticles.Emit(15);
        }
        
        //Damage Popup
        DamagePopup popup = Instantiate(damagePopup,transform.position,Quaternion.identity).GetComponent<DamagePopup>();
        popup.Init(amount, DamagePopup.PopupType.Damage);



        entityData.currentHealth-=amount;
        healthBar.Display(entityData.currentHealth / entityData.maxHealth);
        invincibilityTimer = .2f;
        if(entityData.currentHealth <= 0 ) 
        {
            entityData.currentHealth = 0;
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
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
        entityData.currentHealth = Mathf.Min(entityData.currentHealth, entityData.maxHealth);

        healthBar.Display(entityData.currentHealth / entityData.maxHealth);
        //Damage Popup
        DamagePopup popup = Instantiate(damagePopup, transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        popup.Init(amount, DamagePopup.PopupType.Heal);
    }
    public virtual void HealMana(int amount)
    {
        entityData.currentMana += amount;
        entityData.currentMana = Mathf.Min(entityData.currentMana,entityData.maxMana);

        //Damage Popup
        DamagePopup popup = Instantiate(damagePopup, transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        popup.Init(amount, DamagePopup.PopupType.ManaHeal);
    }

    
}
