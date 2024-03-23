using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int maxHealth;
    public ProjectileData projectile;
    public float attackRange;
    public int damage;
    public float attackSpeed;
    public float moveSpeed;
}
