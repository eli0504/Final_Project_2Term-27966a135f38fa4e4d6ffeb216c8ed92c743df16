using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlayerpref : MonoBehaviour
{
    public string previousScene = "Level2";

    public void EnterHiddenLevel()
    {
        // Save the current scene and the player's position before entering the hidden level
        previousScene = SceneManager.GetActiveScene().name;

         if (string.IsNullOrEmpty(previousScene))
         {
             Debug.LogError("Previous scene name is empty or null.");
             return;
         }

         SceneManager.LoadScene("secretroom");
    }

    public void ExitHiddenLevel()
    {
        //check if previos scene is empty or null
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }

    /*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == previousScene)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                //If the player object is found, restore its position to returnPosition
                player.transform.position = returnPosition;
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }*/
}
