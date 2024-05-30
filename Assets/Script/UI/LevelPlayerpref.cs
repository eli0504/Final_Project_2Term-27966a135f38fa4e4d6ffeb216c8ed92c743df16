using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlayerpref : MonoBehaviour
{
    public string previousScene = "Level2";
    public Vector3 returnPosition = new Vector3(16.80f, 5.55f, 0);

    private OnTrigger onTrigger;

    private void Start()
    {
        onTrigger = GameObject.FindGameObjectWithTag("Player").GetComponent<OnTrigger>();
    }

    public void EnterHiddenLevel()
    {
        // Save the current scene and the player's position before entering the hidden level
        previousScene = SceneManager.GetActiveScene().name;
        returnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (string.IsNullOrEmpty(previousScene))
        {
            Debug.LogError("Previous scene name is empty or null.");
            return;
        }

        SceneManager.LoadScene("secretroom");
    }

    public void ExitHiddenLevel()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == previousScene)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
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
    }
}
