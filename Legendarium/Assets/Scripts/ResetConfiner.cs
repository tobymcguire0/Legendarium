using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ResetConfiner : MonoBehaviour
{
    private void Start() { 
        GetComponent<CinemachineConfiner2D>().InvalidateCache();
        GetComponent<CinemachineVirtualCamera>().m_Follow = FindFirstObjectByType<PlayerController>().transform;
    }

}
