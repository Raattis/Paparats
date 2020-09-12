using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    float flashTimeLeft = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        flashTimeLeft -= Time.deltaTime * 2.0f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = Mathf.Clamp01(flashTimeLeft);
        spriteRenderer.color = color;
    }

    public void flash()
    {
        flashTimeLeft = 1.0f;
    }
}
