using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Celeb : MonoBehaviour
{
	Vector2 originalPosition;

	float progress = 0.0f;

	void Start()
    {
		originalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
	}

    void Update()
	{
		float speed = 1.0f;
		float speedX = 1.0f;
		float pathLengthX = 10.0f;
		progress += Time.deltaTime * speed;
		float y = Mathf.Cos(progress);
		float x = Mathf.PingPong(progress * speedX + pathLengthX * 0.5f, pathLengthX) - pathLengthX * 0.5f;
		transform.localPosition = originalPosition + new Vector2(x, y);
	}
}
