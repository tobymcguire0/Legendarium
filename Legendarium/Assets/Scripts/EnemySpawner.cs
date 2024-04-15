using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Entity
{
    [Header("Spawner Stuff")]
    [SerializeField] PlayerDetector playerDetector;
    [SerializeField] private int spawnerHealth = 5;
    [SerializeField] private GameObject entityToSpawn;
    [SerializeField] float spawnCooldown = 4f;
    [SerializeField] float activationDistance = 6f;
    [SerializeField] LayerMask collisionMask;
    [SerializeField] LootDrops lootDrop;
    public bool spawning = false;
    float cooldownTime;
    protected override void Die()
    {
        lootDrop.DropItem();
        Destroy(this.gameObject);
    }
    private void Start()
    {
        playerDetector.InitializeDetector(this, activationDistance);
        entityData.maxHealth = spawnerHealth;
        entityData.currentHealth = spawnerHealth;
        cooldownTime = spawnCooldown;
    }
    public override void Update()
    {
        base.Update();
        if (!spawning) return;
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
            return;
        }
        if (CanSpawnCheck())
        {
            Instantiate(entityToSpawn, transform.position, Quaternion.identity);
        }
        cooldownTime = spawnCooldown;
    }

    bool CanSpawnCheck()
    {
        RaycastHit2D[] results = new RaycastHit2D[2]; 
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;
        filter.SetLayerMask(collisionMask);
        return (GetComponent<BoxCollider2D>().Cast(Vector2.zero,filter, results, 0)==0);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
