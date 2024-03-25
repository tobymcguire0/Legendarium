using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : KinematicMover
{
    protected Vector2 moveDirection;
    protected Vector2 facingDirection;
    public EnemyData enemyData;
    protected float attackCooldown = 0;
    protected bool attacking;
    protected float stunTime = 0;
    [SerializeField] protected Animator animator;
    protected virtual void Initialize()
    {
        entityData.maxHealth = enemyData.maxHealth;
        entityData.currentHealth = enemyData.maxHealth;
        player = PlayerController.instance;
        moveDirection=Vector2.zero;
        facingDirection = Vector2.right;
    }
    protected abstract void Attack();
    protected void Move()
    {
        if (attacking) return;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        if (Mathf.Abs(direction.x) > 0.1 && Mathf.Abs(direction.y) > 0.1)
        {
            direction = new Vector2(Mathf.Sign(direction.x), Mathf.Sign(direction.y));
            direction.Normalize();
        } else
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction.y = 0;
            } else
            {
                direction.x = 0;
            }
        }
        if (direction != Vector2.zero)
            facingDirection = direction;
        Vector2 nextPosition = direction * enemyData.moveSpeed * Time.deltaTime;
        KinematicMove(nextPosition);
    }
    protected PlayerController player;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Initialize();
    }
    public override void Damage(int amount,Vector2 knockback)
    {
        float oldInvincibilityTimer = invincibilityTimer;
        base.Damage(amount, knockback);
        if (oldInvincibilityTimer > 0) return;
        KinematicMove(knockback*.3f);
        animator.SetTrigger("Stun");
        stunTime = .3f;
    }
    public override void Update()
    {
        base.Update();
        UnStuck();
        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;

        if (stunTime > 0)
        {
            stunTime-= Time.deltaTime;
            return;
        }
        
        if(Vector2.Distance(player.transform.position,transform.position)<=enemyData.attackRange)
        {
            if (attackCooldown <= 0 && !attacking)
            {
                attacking = true;
                animator.SetTrigger("Attack");
            }

        } else
        {
            Move();
        }
    }
}
