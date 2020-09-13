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

    private void OnGUI()
    {
        //GUIStyle style = GUIStyle.none;
        //style.normal.textColor = Color.red;
        //style.fontStyle = FontStyle.Bold;
        //style.fontSize = 40;
        //style.alignment = TextAnchor.MiddleCenter;
        //
        //Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, -transform.position.y));
        //GUI.Label(new Rect(screenPoint.x - 500.0f, screenPoint.y - 200.0f, 1000.0f, 100.0f), "" + Mathf.RoundToInt(parallaxScale * 100), style);
    }
}
