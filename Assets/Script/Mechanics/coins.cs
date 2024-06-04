using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class coins : MonoBehaviour
{
    //COINS ID
    public string coinID; // The unique identifier for coins


    private void Start()
    {
        if (GameManager.instance == null)
        {
            return;
        }

        // destroy coin if it's collected
        if (GameManager.instance.IsCoinCollected(coinID))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //coins
        if (other.gameObject.tag == "Player")
        {
            if(GameManager.instance != null)
            {
                GameManager.instance.CollectCoin(coinID);
                Destroy(gameObject);
            }
        }
    }

}
