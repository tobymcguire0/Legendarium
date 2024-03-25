using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Entity
{
    [SerializeField] PlayerDetector playerDetector;
    [SerializeField] private int spawnerHealth = 5;
   [SerializeField] private GameObject entityToSpawn;
    [SerializeField] float spawnCooldown = 4f;
    [SerializeField] float activationDistance = 6f;
    public bool spawning = false;
    float cooldownTime;
    protected override void Die()
    {
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
        Instantiate(entityToSpawn, transform.position, Quaternion.identity);
        cooldownTime = spawnCooldown;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}
