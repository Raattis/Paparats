using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Celeb : MonoBehaviour
{
	Vector2 originalPosition;

	void Start()
    {
		originalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
	}

    void Update()
    {
		transform.localPosition = originalPosition + new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time));
	}
}
