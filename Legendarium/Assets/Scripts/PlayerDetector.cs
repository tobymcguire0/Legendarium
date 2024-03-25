using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    EnemySpawner spawner;
    public void InitializeDetector(EnemySpawner spawner,float activationDistance)
    {
        this.spawner = spawner;
        GetComponent<CircleCollider2D>().radius = activationDistance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.spawning = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawner.spawning = true;
        }
    }
}
