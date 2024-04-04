using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Projectile : MonoBehaviour
{
    [SerializeField] ProjectileData data;
    Vector2 projectileDirection;
    bool hitObject = false;
    float life;
    public void Init(Vector2 direction)
    {
        life = 3;
        projectileDirection = direction;
    }
    private void Update()
    {
        if (hitObject) return;
        life -= Time.deltaTime;
        if (life <= 0)
        {
            HitObject();
        }
        else
        {
            transform.position += (Vector3)(projectileDirection * data.ProjectileSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")||collision.isTrigger)
            return;
        Entity hitEntity;
        if(collision.gameObject.TryGetComponent(out hitEntity))
        {
            Debug.Log("HEEHHEH");
            hitEntity.Damage(data.ProjectileDamage,(collision.transform.position-transform.position).normalized);
        }
        HitObject();
    }
    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
    void HitObject()
    {
        hitObject = true;
        GetComponent<Animator>().SetTrigger("Explode");
    }
}
