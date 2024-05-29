using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlayerpref : MonoBehaviour
{
    public string previousScene = "Level2";
    public Vector3 returnPosition = new Vector3(16.80f, 5.55f, 0);

    private OnTrigger onTrigger;

    int collectedCoins = 0;

    private void Start()
    {
        onTrigger = GameObject.FindGameObjectWithTag("Player").GetComponent<OnTrigger>();
    }

    public void EnterHiddenLevel()
    {
        // Guardar las monedas recogidas antes de entrar en el nivel oculto
        SaveCollectedCoins();

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

            // Eliminar las monedas que ya fueron recogidas en el nivel anterior
            RemoveCollectedCoins();
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

    void SaveCollectedCoins()
    {
        // Guardar las monedas recogidas en PlayerPrefs
        if (onTrigger != null)
        {
            collectedCoins = onTrigger.coinsCollected;
        }
            
        PlayerPrefs.SetInt("CollectedCoins", collectedCoins);
    }

    void RemoveCollectedCoins()
    {
        // Eliminar las monedas recogidas guardadas en PlayerPrefs
        PlayerPrefs.DeleteKey("CollectedCoins");
    }
}
