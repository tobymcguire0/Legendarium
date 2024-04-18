using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject UI;
    [SerializeField] TMP_Text winScreenScore;
    [SerializeField] GameObject WinScreen;
    int score = 0;
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
    void AddPoints(int amount)
    {
        score += amount;
    }
    private void OnEnable()
    {
        Entity.onAwardPoints += AddPoints;
        HealthPickup.onAwardPoints += AddPoints;

        PlayerController.PlayerDeath += OnPlayerDeath;
        WinScreen.SetActive(false);
    }
    private void OnDisable()
    {
        Entity.onAwardPoints -= AddPoints;
        HealthPickup.onAwardPoints -= AddPoints;
        PlayerController.PlayerDeath -= OnPlayerDeath;
    }
    void OnPlayerDeath()
    {
        score = 0;
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
    public static void StartEndSequence()
    {
        instance.UI.SetActive(false);
        instance.WinScreen.SetActive(true);
        instance.winScreenScore.text = "FINAL SCORE - "+instance.score.ToString();
    }
}
