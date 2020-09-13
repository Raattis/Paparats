using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [System.NonSerialized]
    public float x = 0;
    [System.NonSerialized]
    public float y = 0;

    public float minX = -20.0f;
    public float maxX = 20.0f;
	public float minY = -3.0f;
	public float maxY = 3.0f;
	private Vector2 speed = new Vector2(3.0f, 1.0f);

    private Vector2 touchStart = Vector2.zero;
    private int touchId = -1;

    void Start()
    {
    }

    void Update()
    {
        FinalScore finalScore = FindObjectOfType<FinalScore>();
        if (finalScore == null || finalScore.isGameOver)
            return;

        Vector2 touchDelta = Vector2.zero;
        if (Input.touchCount > 0)
        {
            Vector2 delta = (Input.touches[0].position - touchStart) / Camera.main.pixelHeight * 4.0f;
            if (Input.touches[0].phase == TouchPhase.Ended && delta.magnitude < 0.002f && Input.touches[0].fingerId == touchId)
            {
                Photo.TakePhoto();
            }
            else if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStart = Input.touches[0].position;
                touchId = Input.touches[0].fingerId;
            }
            else if (Input.touches[0].fingerId == touchId)
            {
                touchDelta = delta;
            }

        }
        else if (touchId == -1)
        {
            Vector2 mouseDelta = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - touchStart) / Camera.main.pixelHeight * 4.0f;
            if (Input.GetMouseButtonUp(0) && mouseDelta.magnitude < 0.002f)
            {
                Photo.TakePhoto();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                touchStart = Input.mousePosition;
                touchId = -1;
            }
            else if (Input.GetMouseButton(0))
            {
                touchDelta = mouseDelta;
            }
        }

        touchDelta.x += Input.GetAxis("Horizontal");
        touchDelta.y += Input.GetAxis("Vertical");

        if (touchDelta.magnitude > 1.0f)
        {
            touchDelta = touchDelta / touchDelta.magnitude;
        }

        x += Time.deltaTime * speed.x * touchDelta.x;
        y += Time.deltaTime * speed.y * touchDelta.y;

		x = Mathf.Clamp(x, minX, maxX);
		y = Mathf.Clamp(y, minY, maxY);
	}
}
