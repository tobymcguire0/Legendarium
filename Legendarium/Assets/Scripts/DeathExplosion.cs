using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplosion : MonoBehaviour
{

    public void OnAnimationEnd()
    {
        Destroy(this.gameObject);
    }
}
