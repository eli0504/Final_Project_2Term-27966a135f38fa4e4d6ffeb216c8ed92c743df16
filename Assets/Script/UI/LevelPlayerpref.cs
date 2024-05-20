using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPlayerpref : MonoBehaviour
{
    public string previousScene;
    public Vector3 returnPosition;

    public void EnterHiddenLevel()
    {
        // Guardar la escena actual y la posición del jugador antes de entrar al nivel oculto
        previousScene = SceneManager.GetActiveScene().name;
        returnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Cargar el nivel oculto
        SceneManager.LoadScene("secretroom");
    }

    public void ExitHiddenLevel()
    {
        // Cargar la escena anterior
        SceneManager.LoadScene(previousScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == previousScene)
        {
            // Colocar al jugador en la posición guardada
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = returnPosition;
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
