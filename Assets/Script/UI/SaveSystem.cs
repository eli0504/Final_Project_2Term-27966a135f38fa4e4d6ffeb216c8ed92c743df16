using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    public string playerHealthKey = "PlayerHealth", sceneKey = "SceneIndex", savePresentKey = "SavePresent";
    public LoadedData loadedData { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); //for persist the data in the new scene
    }

    //called when we have entered the main menu 
    private void Start()
    {
        var result = LoadData();
    }

    public void ResetData() //delete all info for start a new Game
    {
        PlayerPrefs.DeleteKey(playerHealthKey);
        PlayerPrefs.DeleteKey(sceneKey);
        PlayerPrefs.DeleteKey(savePresentKey);
        loadedData = null;
    }

    //load stored game data
    public bool LoadData()
    {
        if(PlayerPrefs.GetInt(savePresentKey) == 1) //If there is saved data...
        {
            loadedData = new LoadedData();
            loadedData.playerHealth = PlayerPrefs.GetInt(playerHealthKey);
            loadedData.sceneIndex = PlayerPrefs.GetInt(sceneKey);
            return true; //The data was loaded successfully
        }
        return false; //If it is not 1 there is no data to load.
    }

    public void SaveData(int sceneIndex, int playerHealth)
    {
        //important, we save the data before new scene is loaded
        if (loadedData == null)
            loadedData = new LoadedData();
        loadedData.playerHealth = playerHealth;
        loadedData.sceneIndex = sceneIndex;
        PlayerPrefs.SetInt(playerHealthKey, playerHealth);
        PlayerPrefs.SetInt(sceneKey, sceneIndex);
        PlayerPrefs.SetInt(savePresentKey, 1);
    }
}

//store the data that we have loaded
public class LoadedData
{
    public int playerHealth = -1; //default
    public int sceneIndex = -1;
}
