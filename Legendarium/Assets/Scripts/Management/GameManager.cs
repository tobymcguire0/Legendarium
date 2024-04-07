using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.PlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDeath -= OnPlayerDeath;
    }
    void OnPlayerDeath()
    {
        StartCoroutine(WaitToRestart());
    }
    IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}
