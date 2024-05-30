using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // List to save the IDs of the collected coins
    private HashSet<string> collectedCoins = new HashSet<string>();

    //PlayerData
    private GameObject player;
    private SaveSystem saveSystem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += Initialize;
        }
        else
        {
            Destroy(gameObject);
        }

        //SceneManager.sceneLoaded += Initialize;
       // DontDestroyOnLoad(gameObject);
    }

    private void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded");

        var playerController = FindObjectOfType<PlayerController>();

        if (playerController != null)
        {
            player = playerController.gameObject;

            saveSystem = FindObjectOfType<SaveSystem>();
            if (saveSystem != null && saveSystem.loadedData != null)
            {
                var damagable = player.GetComponent<Health>();
                if (damagable != null)
                {
                    damagable.numberOfHearts = saveSystem.loadedData.playerHealth;
             
                }
            }
        }
    }
   
   public void SaveData() //save player health for next level
    {
        if (player != null && saveSystem != null)
        {
            saveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1, player.GetComponent<Health>().numberOfHearts);
        }
    }

    // Method for recording a collected coin
    public void CollectCoin(string coinID)
    {
        collectedCoins.Add(coinID);
    }

    // Method to verify if a coin has been collected
    public bool IsCoinCollected(string coinID)
    {
        return collectedCoins.Contains(coinID);
    }

    public void ResetCollectedCoins()
    {
        collectedCoins.Clear();
    }
}
