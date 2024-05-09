using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeFall2 : MonoBehaviour
{
    private float speed = 1f;
    private float height = -8.67f;
    private float startY = 0.31f;

    void Update()
    {
        var pos = transform.position;
        var newY = startY + height * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}
