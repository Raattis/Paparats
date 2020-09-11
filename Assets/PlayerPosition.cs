using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [System.NonSerialized]
    public float x = 0;
    [System.NonSerialized]
    public float y = 0;

    public float minX = -1.0f;
    public float maxX = 1.0f;
    private Vector2 speed = new Vector2(3.0f, 1.0f);

    void Start()
    {
        
    }

    void Update()
    {
        x += Time.deltaTime * speed.x * Input.GetAxis("Horizontal");
        y += Time.deltaTime * speed.y * Input.GetAxis("Vertical");
    }
}
