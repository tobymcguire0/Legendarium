using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData", order = 1)]
public class ProjectileData : ScriptableObject
{
    public float ProjectileSpeed = 7f;
    public int ProjectileDamage = 10;
}
