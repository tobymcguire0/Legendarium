using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public List<string> ignoreTags;
    int damage;
    public void InitHurtbox(int damage)
    {
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignoreTags.Contains(collision.tag) || collision.isTrigger) return;
        Entity hitEntity;
        if(collision.TryGetComponent<Entity>(out hitEntity))
        {
            hitEntity.Damage(damage,(hitEntity.transform.position-transform.position).normalized);
        }
    }
}
