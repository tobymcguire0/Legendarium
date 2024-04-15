using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerController : KinematicMover
{
    public static PlayerController instance;
    PlayerStateFactory states;
    PlayerState currentState;
    [Header("Player Controller Stuff")]
    [SerializeField] GameObject magicProjectile;
    [SerializeField] ParticleSystem fizzleParticles;
    [SerializeField] ParticleSystem magicParticles;
    [SerializeField] Transform attackRotator;
    [SerializeField] BoxCollider2D meleeHurtbox;
    [SerializeField] CharacterType characterData;
    [SerializeField] Animator animator;
    [SerializeField] float baseMoveSpeed;
    [SerializeField] int globalBaseMeleeDamage;
    [SerializeField] float invincibilityTime=.2f;
    Controls controls;

    Vector2 facingDirection;
    Vector2 movingDirection;
    float damageTimer;
    bool magicPressed = false;
    bool meleePressed = false;
    int numKeys;


    public delegate void HealthChangeEvent(float percent);
    public delegate void DeathEvent();
    public static HealthChangeEvent OnPlayerHealthChange; 
    public static HealthChangeEvent OnPlayerManaChange;
    public static DeathEvent PlayerDeath;
    public bool IsMagicPressed { get { return magicPressed; } }
    public bool IsMeleePressed { get { return meleePressed; } }
    public Vector2 FacingDirection { get { return facingDirection; } set { facingDirection = value; attackRotator.right = value; } }
    public Vector2 MovingDirection { get { return movingDirection; } }
    public float MoveSpeed { get { return baseMoveSpeed; } }
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterType CharacterData { get { return characterData; } }
    public Animator CharacterAnimator { get { return animator; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
            return;
        }
        states = new PlayerStateFactory(this);
        currentState = states.Idle();
        rb = GetComponent<Rigidbody2D>();
        MeleeHurtbox(false);
        meleeHurtbox.GetComponentInChildren<Hurtbox>().InitHurtbox((int)(characterData.baseMeleeDamage*globalBaseMeleeDamage));
        entityData.currentMana = characterData.maxMana;
        entityData.maxMana = characterData.maxMana;
        entityData.currentHealth = characterData.maxHealth;
        entityData.maxHealth = characterData.maxHealth;
    }

    public void FireProjectile()
    {
        if (entityData.currentMana <= 0)
        {
            fizzleParticles.Emit(5);
            return;
        }
        magicParticles.Emit(15);
        entityData.currentMana--;
        OnPlayerManaChange?.Invoke(entityData.currentMana / entityData.maxMana);
        Projectile proj = Instantiate(magicProjectile).GetComponent<Projectile>();
        proj.transform.position = transform.position+(Vector3)facingDirection;
        proj.Init(facingDirection);
    }
    public void MeleeHurtbox(bool enabled)
    {
        meleeHurtbox.enabled = enabled;
        meleeHurtbox.gameObject.SetActive(enabled);
    }

    public void pickupKey(int number)
    {
        numKeys+=number;
    }
    public bool UseKey()
    {
        if (numKeys <= 0) return false;
        numKeys--;
        return true;
    }
    void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        movingDirection = ctx.ReadValue<Vector2>();
        if (movingDirection.x != 0 && movingDirection.y != 0)
        {
            movingDirection = new Vector2(1*Mathf.Sign(movingDirection.x), 1 * Mathf.Sign(movingDirection.y));            
        }
        movingDirection.Normalize();
        if (movingDirection != Vector2.zero)
        {
            facingDirection = movingDirection;
            float angle = Vector2.Angle(Vector2.right, facingDirection);
            if (Vector2.Dot(Vector2.up, facingDirection) < 0) angle = 360-angle;
            int facingIndex = Mathf.FloorToInt(angle*8/360);
            CharacterAnimator.SetInteger("FacingDirection", facingIndex);
        }
        
    }

    public override void Damage(int amount,Vector2 knockback)
    {
        if (damageTimer > 0) return;
        if(entityData.currentHealth>0)
            damageTimer = invincibilityTime;
        base.Damage(amount,knockback);
        OnPlayerHealthChange?.Invoke(entityData.currentHealth / entityData.maxHealth);
        
    }

    public override void Heal(int amount)
    {
        base.Heal(amount); 
        OnPlayerHealthChange?.Invoke(entityData.currentHealth / entityData.maxHealth);
    }
    public override void HealMana(int amount)
    {
        base.HealMana(amount); 
        OnPlayerManaChange?.Invoke(entityData.currentMana / entityData.maxMana);
    }
    protected override void Die()
    {
        Debug.Log("Player Died!");
        PlayerDeath?.Invoke();
        Destroy(gameObject);
    }

    public override void Update()
    {
        base.Update();
        UnStuck();
        if (damageTimer > 0) damageTimer -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        currentState?.Update(Time.fixedDeltaTime);
    }
    void OnMagicPressed(InputAction.CallbackContext ctx)
    {
        magicPressed = ctx.ReadValueAsButton();
    }

    void OnMeleePressed(InputAction.CallbackContext ctx)
    {
        meleePressed = ctx.ReadValueAsButton();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMovePerformed;
        controls.Player.Melee.performed -= OnMeleePressed;
        controls.Player.Melee.canceled -= OnMeleePressed;
        controls.Player.Magic.performed -= OnMagicPressed;
        controls.Player.Magic.canceled -= OnMagicPressed;
    }
    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMovePerformed;
        controls.Player.Melee.performed += OnMeleePressed;
        controls.Player.Melee.canceled += OnMeleePressed;
        controls.Player.Magic.performed += OnMagicPressed;
        controls.Player.Magic.canceled += OnMagicPressed;
    }
}
