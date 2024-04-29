using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;

    //the item will be instantiating a UI button with the image of the item and you can use it clicking the button
    public GameObject itemButton;


    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //check if the inventory has some empty slots or it´s already full
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                //check if the inventory is not full
                if (inventory.isFull[i] == false)
                {
                    //item can be added to inventory
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false); //the button spawns at the same pos has my inventory slot and FALSE because I don´t want my button to be in the world coordinates
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
