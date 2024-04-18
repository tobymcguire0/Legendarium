using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player;
            if(collision.TryGetComponent<PlayerController>(out player))
            {
                if (player.UseKey())
                {
                    Debug.Log("Unlocking Door");
                    Unlock();
                }
            }
        }
    }
    void Unlock()
    {
        GetComponent<ParticleSystem>()?.Play();
        Destroy(this.transform.parent.gameObject);
    }
}
