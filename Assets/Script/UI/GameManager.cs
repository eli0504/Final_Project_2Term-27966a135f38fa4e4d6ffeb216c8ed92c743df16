using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //PlayerData
    public GameObject player;
    public SaveSystem saveSystem;

    private void Awake()
    {
        SceneManager.sceneLoaded += Initialize;
        DontDestroyOnLoad(gameObject);
    }

    private void Initialize(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log("Loaded");

        var playerController = FindObjectOfType<PlayerController>();

        if (playerController != null)
        {
            player = playerController.gameObject;

            saveSystem = FindObjectOfType<SaveSystem>();

            if (player != null && saveSystem.loadedData != null)
            {
                var damagable = player.GetComponent<Health>();
                damagable.numberOfHearts = saveSystem.loadedData.playerHealth; //Updates the player's number of hearts with the loaded value
            }
        }
    }
   
   public void SaveData() //save player health for next level
    {
        if(player != null)
        {
            saveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1, player.GetComponent<Health>().numberOfHearts);
        }
    }
}
