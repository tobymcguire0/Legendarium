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
    [SerializeField] GameObject magicProjectile;
    [SerializeField] Transform attackRotator;
    [SerializeField] BoxCollider2D meleeHurtbox;
    [SerializeField] CharacterType characterData;
    [SerializeField] float baseMoveSpeed;
    [SerializeField] int globalBaseMeleeDamage;
    [SerializeField] float invincibilityTime=.2f;
    Controls controls;

    Vector2 facingDirection;
    Vector2 movingDirection;
    float damageTimer;
    bool magicPressed = false;
    bool meleePressed = false;
    public bool IsMagicPressed { get { return magicPressed; } }
    public bool IsMeleePressed { get { return meleePressed; } }
    public Vector2 FacingDirection { get { return facingDirection; } set { facingDirection = value; attackRotator.right = value; } }
    public Vector2 MovingDirection { get { return movingDirection; } }
    public float MoveSpeed { get { return baseMoveSpeed; } }
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterType CharacterData { get { return characterData; } }

    private void Awake()
    {
        instance = this;
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
        Projectile proj = Instantiate(magicProjectile).GetComponent<Projectile>();
        proj.transform.position = transform.position+(Vector3)facingDirection;
        proj.Init(facingDirection);
    }
    public void MeleeHurtbox(bool enabled)
    {
        meleeHurtbox.enabled = enabled;
        meleeHurtbox.gameObject.SetActive(enabled);
    }


    void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        movingDirection = ctx.ReadValue<Vector2>();
        if (movingDirection.x != 0 && movingDirection.y != 0)
        {
            movingDirection = new Vector2(1*Mathf.Sign(movingDirection.x), 1 * Mathf.Sign(movingDirection.y));            
        }
        movingDirection.Normalize();
        
    }

    public override void Damage(int amount,Vector2 knockback)
    {
        if (damageTimer > 0) return;
        if(entityData.currentHealth>0)
            damageTimer = invincibilityTime;
        base.Damage(amount,knockback);
        
    }
    protected override void Die()
    {
        Debug.Log("Player Died!");
        Destroy(gameObject);
    }

    public override void Update()
    {
        base.Update();
        UnStuck();
        if (damageTimer > 0) damageTimer -= Time.deltaTime;
        currentState?.Update(Time.deltaTime);
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
