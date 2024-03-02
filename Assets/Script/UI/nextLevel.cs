using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class nextLevel : MonoBehaviour
{
    GameManager gameManager;
   
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) //Checks if the layer of the colliding object is present in the playerMask
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.SaveData();
            gameManager.LoadNextLevel();
        }
    }
}
