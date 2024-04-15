using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : KinematicMover
{
    [Header("Enemy Stuff")]
    protected Vector2 moveDirection;
    protected Vector2 facingDirection;
    public EnemyData enemyData;
    [SerializeField] protected LootDrops lootDrop;
    [SerializeField] protected GameObject[] mustDropList;
    protected float attackCooldown = 0;
    protected bool attacking;
    bool moving = false;
    protected float stunTime = 0;
    [SerializeField] protected Animator animator;

    Vector2[] validDirections = { Vector2.right, new Vector2(1, 1).normalized, Vector2.up, new Vector2(-1, 1).normalized, Vector2.left, new Vector2(-1, -1), Vector2.down, new Vector2(1, -1).normalized };
    private void OnEnable()
    {
        PlayerController.PlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDeath -= OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        this.enabled = false;
    }

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
        if (attacking)
        {
            animator.SetBool("Moving", false);
        }
        Vector2 direction = (player.transform.position - transform.position).normalized;
        if (direction != Vector2.zero)
        {
            animator.SetBool("Moving", true);
            facingDirection = direction;
            float angle = Vector2.Angle(Vector2.right, facingDirection);
            if (Vector2.Dot(Vector2.up, facingDirection) < 0) angle = 360-angle;
            int facingIndex = Mathf.FloorToInt(angle * 8 / 360);
            direction = validDirections[facingIndex];
            animator.SetInteger("MoveDirection", facingIndex);
        }
        Vector2 nextPosition = direction * enemyData.moveSpeed * Time.fixedDeltaTime;
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
        animator.SetBool("Stun",true);
        animator.SetBool("Moving", false);
        stunTime = .5f;
    }
    public override void Update()
    {
        base.Update();
        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;

        if (stunTime > 0)
        {
            stunTime-= Time.deltaTime;
            if (stunTime <= 0)
            {
                animator.SetBool("Stun", false);
            }
            return;
        }
        
        if(Vector2.Distance(player.transform.position,transform.position)<=enemyData.attackRange)
        {
            if (attackCooldown <= 0 && !attacking)
            {
                attacking = true;
            }

        }

        animator.SetBool("Attack", attacking);
    }
    private void FixedUpdate()
    {
        UnStuck();
        if (!attacking && stunTime<=0)
        {
            Move();
        }
    }
}
