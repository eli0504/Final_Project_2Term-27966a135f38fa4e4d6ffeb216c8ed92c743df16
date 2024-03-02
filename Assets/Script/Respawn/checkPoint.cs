using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private Respawn respawn;
    private BoxCollider2D checkPointCollider;
    private void Awake()
    {
        checkPointCollider = GetComponent<BoxCollider2D>();
        respawn = GameObject.FindGameObjectWithTag("respawn").GetComponent<Respawn>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioLibrary.PlaySound("point");
            respawn.respawnoint = this.gameObject;
            checkPointCollider.enabled = false;//disable the last checkpoint
        }
    }
}
