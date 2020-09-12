using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(Random.Range(0.5f, 1.0f), Random.Range(0.5f, 1.0f), Random.Range(0.85f, 1.0f), 1.0f);
        GetComponent<Parallax>().parallaxScale = 1.0f - Mathf.Clamp((9.5f + transform.localPosition.y) * 0.07f, 0.00f, 0.08f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
