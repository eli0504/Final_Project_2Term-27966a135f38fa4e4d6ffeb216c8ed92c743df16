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
        // Destruir la moneda si ya ha sido recogida
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
                GameManager.instance.CollectCoin(coinID);
                Destroy(gameObject);
        }
    }

}
