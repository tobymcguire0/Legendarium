using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public delegate void AwardPointsOnPickup(int numPoints);
    public static AwardPointsOnPickup onAwardPoints;
    enum PickupType
    {
        Health,
        Mana,
        Key,
    }
    [SerializeField] PickupType type;
    [SerializeField] int healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger && collision.CompareTag("Player"))
        {
            Entity entity=collision.GetComponent<Entity>();
            switch (type)
            {
                case PickupType.Health:
                    entity.Heal(healAmount);
                    break;
                case PickupType.Mana:
                    entity.HealMana(healAmount);
                    break;
                case PickupType.Key:
                    entity.GetComponent<PlayerController>().pickupKey(1);
                    break;

            }
            onAwardPoints?.Invoke(50);
            Destroy(this.gameObject);
        }
    }
}
