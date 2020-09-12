using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxScale = 1.0f;

    [System.NonSerialized]
    public Vector3 originalPos;
    PlayerPosition playerPosition;

    void Start()
    {
        playerPosition = GameObject.FindObjectOfType<PlayerPosition>();
        originalPos = transform.localPosition;

        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.sortingOrder = (int)(parallaxScale * 1000.0f);
        }
    }

    void Update()
    {
        transform.localPosition = originalPos + new Vector3(-playerPosition.x, -playerPosition.y) * parallaxScale;
    }
}
