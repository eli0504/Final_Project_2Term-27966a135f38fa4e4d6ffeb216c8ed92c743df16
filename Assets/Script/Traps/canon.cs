using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;

    private float timeBetween;
    public float startTimeBetween;

    private void Start()
    {
        timeBetween = startTimeBetween;
    }

    private void Update()
    {
        if(timeBetween <= 0)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            timeBetween = startTimeBetween;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }
}
