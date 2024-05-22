using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCollect : MonoBehaviour
{
    public TextMeshProUGUI coinsCounterText;
    public int coinsNeeded = 27;
    private int coinsCollected = 0;
    private int coinsCounter = 0;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "coins")
        {
            coinsCollected++;
            coinsCounter++;
            audioLibrary.PlaySound("coin");
            Destroy(other.gameObject); // Hace que el objeto desaparezca
        }
    }
}

