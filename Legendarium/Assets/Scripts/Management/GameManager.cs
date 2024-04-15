using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else
        {
            Destroy(this.gameObject);
            return;
        }
    }
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
        instance = null;
        PlayerController.instance = null;
        Destroy(this.gameObject);
    }
}
