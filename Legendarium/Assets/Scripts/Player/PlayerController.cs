using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerController : Entity
{
    PlayerStateFactory states;
    PlayerState currentState;
    [SerializeField] GameObject magicProjectile;
    [SerializeField] Transform attackRotator;
    [SerializeField] BoxCollider2D meleeHurtbox;
    [SerializeField] CharacterType characterData;
    [SerializeField] float baseMoveSpeed;
    [SerializeField] float invincibilityTime=.2f;
    Controls controls;

    Vector2 facingDirection;
    Vector2 movingDirection;
    float damageTimer;

    public bool IsMagicPressed { get { return controls.Player.Magic.IsPressed(); } }
    public bool IsMeleePressed { get { return controls.Player.Melee.IsPressed(); } }
    public Vector2 FacingDirection { get { return facingDirection; } set { facingDirection = value; attackRotator.right = value; } }
    public Vector2 MovingDirection { get { return movingDirection; } }
    public float MoveSpeed { get { return baseMoveSpeed; } }
    public PlayerState CurrentState { get { return currentState; } set { currentState = value; } }
    public CharacterType CharacterData { get { return characterData; } }

    private void Awake()
    {
        states = new PlayerStateFactory(this);
        currentState = states.Idle();
        MeleeHurtbox(false);
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
        if (movingDirection.x != 0) movingDirection.y = 0;
        movingDirection.Normalize();
        
    }

    public override void Damage(int amount)
    {
        if (damageTimer > 0) return;
        if(entityData.currentHealth>0)
            damageTimer = invincibilityTime;
        base.Damage(amount);
        
    }
    protected override void Die()
    {
        Debug.Log("Player Died!");
        Destroy(gameObject);
    }

    public void Update()
    {
        if (damageTimer > 0) damageTimer -= Time.deltaTime;
        currentState?.Update();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMovePerformed;
    }
    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMovePerformed;
    }
}
