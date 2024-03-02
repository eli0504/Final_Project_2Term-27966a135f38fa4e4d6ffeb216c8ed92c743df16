using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }
    public void IsGameOver()
    {
        Time.timeScale = 0f; // stop time
        gameOverPanel.SetActive(true);
        audioLibrary.PlaySound("gameOverSound");
    }
}
