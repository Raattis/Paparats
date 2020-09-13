using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public List<Sprite> sprites;

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color color =  spriteRenderer.color;
        color.g = Random.Range(0.7f, 1.0f);
        spriteRenderer.color = color;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
        //GetComponent<Parallax>().parallaxScale = 1.0f - Mathf.Clamp((9.5f + transform.localPosition.y) * 0.07f, 0.00f, 0.08f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
