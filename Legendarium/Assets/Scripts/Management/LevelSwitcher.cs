using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] string nextLevel;
    bool transitioning = false;
    public void SwitchLevel(string levelName)
    {
        if(transitioning) return;
        transitioning = true;
        SceneManager.LoadScene(levelName);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            SwitchLevel(nextLevel);
        }
    }
}
