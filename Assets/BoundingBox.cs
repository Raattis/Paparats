using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Box
{
    public float x;
    public float y;
    public float w;
    public float h;


    public Vector2 topLeft { get { return new Vector2(x, y); } }
    public Vector2 topRight { get { return new Vector2(x + w, y); } }
    public Vector2 bottomLeft { get { return new Vector2(x, y + h); } }
    public Vector2 bottomRight { get { return new Vector2(x + w, y + h); } }

    public Box(float x, float y, float w, float h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
    public Box(Vector2 pos, Vector2 size)
    {
        x = pos.x;
        y = pos.y;
        w = size.x;
        h = size.y;
    }

    public float getArea()
    {
        return Mathf.Max(w, 0.0f) * Mathf.Max(h, 0.0f);
    }

    public Box clip(Box other)
    {
        float minX = Mathf.Max(x, other.x);
        float minY = Mathf.Max(y, other.y);
        float maxX = Mathf.Min(x + w, other.x + other.w);
        float maxY = Mathf.Min(y + h, other.y + other.h);
        return new Box(minX, minY, Mathf.Max(0.0f, maxX - minX), Mathf.Max(0.0f, maxY - minY));
    }

    public float overlap(Box other)
    {
        float area = Mathf.Max(w, 0.0001f) * Mathf.Max(h, 0.0001f);
        float clippedArea = this.clip(other).getArea();
        return clippedArea / area;
    }

    public void draw(Color color)
    {
        Debug.DrawLine(topLeft, topRight, color);
        Debug.DrawLine(topLeft, bottomLeft, color);
        Debug.DrawLine(topRight, bottomRight, color);
        Debug.DrawLine(bottomRight, bottomLeft, color);
    }

    public void onGuiBox(string text)
    {
        if (true)
            return;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector2(bottomLeft.x, -bottomLeft.y));
        Vector3 screenPoint2 = Camera.main.WorldToScreenPoint(new Vector2(topRight.x, -topRight.y));
        GUIStyle style = GUIStyle.none;
        style.normal.textColor = Color.white;
        style.fontSize = 30;

        GUI.Box(new Rect(screenPoint, screenPoint2 - screenPoint), text);

    }
};

public class BoundingBox : MonoBehaviour
{
	public Vector2 pos { get { return new Vector2(transform.position.x, transform.position.y) - size * 0.5f; } }
	public Vector2 size { get { return new Vector2(transform.lossyScale.x, transform.lossyScale.y) * 0.136f; } }
    public Box boundingBox { get { return new Box(pos, size); } }

    public bool obstruction = false;
    public bool dontHideSprite = false;

	void Awake()
    {
        if (!dontHideSprite)
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
                spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (obstruction)
            boundingBox.draw(new Color(1, 0, 1, 1));
        else
            boundingBox.draw(Color.green);
    }
}
