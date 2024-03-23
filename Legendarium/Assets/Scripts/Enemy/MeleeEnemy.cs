using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] GameObject AttackRotator;
    private void Awake()
    {
        AttackRotator.SetActive(false);
        AttackRotator.GetComponentInChildren<Hurtbox>().InitHurtbox(enemyData.damage);
    }
    protected override void Attack()
    {
        AttackRotator.transform.right = facingDirection;
        StartCoroutine(Swing());
    }
    public void EndAttackAnim()
    {
        attacking = false;
        attackCooldown = .5f;
    }
    IEnumerator Swing()
    {
        AttackRotator.SetActive(true);
        yield return new WaitForSeconds(.1f);
        AttackRotator.SetActive(false);
    }

    protected override void Die()
    {
        Destroy(this.gameObject);
    }

    
}
