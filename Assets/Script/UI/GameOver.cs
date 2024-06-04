using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private OnTrigger onTrigger;
    public GameObject gameOverPanel;

    void Start()
    {
        onTrigger = GetComponent<OnTrigger>();
        gameOverPanel.SetActive(false);
    }
    public void IsGameOver()
    {
        onTrigger.QuitRememberPanel();
        Time.timeScale = 0f; // stop time
        gameOverPanel.SetActive(true);
        audioLibrary.PlaySound("gameOverSound");

        GameManager.instance.ResetCollectedCoins();
    }
}
