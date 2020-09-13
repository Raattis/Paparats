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
		float pathLengthX = 20.0f;
		progress += Time.deltaTime * speed;
		float y = Mathf.Cos(progress + Mathf.PI * 0.75f);
		float x = Mathf.PingPong(progress * speedX + pathLengthX * 0.5f, pathLengthX) - pathLengthX * 0.5f;
		x += Mathf.Sin(progress * 3.0f) * 0.5f;
		transform.localPosition = originalPosition + new Vector2(x, y);
	}

	public float GetScore()
	{
		float headScore = 0.0f;
		float feetScore = 0.0f;
		float bodyScore = 0.0f;
		float tailScore = 0.0f;

		foreach (CelebBoundingBox celebBoundingBox in GetComponentsInChildren<CelebBoundingBox>())
		{
			switch (celebBoundingBox.bodyPart)
			{
				case CelebBoundingBox.BodyPart.Feet: feetScore = celebBoundingBox.score; break;
				case CelebBoundingBox.BodyPart.Body: bodyScore = celebBoundingBox.score; break;
				case CelebBoundingBox.BodyPart.Tail: tailScore = celebBoundingBox.score; break;
				case CelebBoundingBox.BodyPart.Head: headScore = celebBoundingBox.score; break;
			}
		}

		float maxScore = 100.0f;
		maxScore *= Mathf.Lerp(0.9f, 1.3f, feetScore);
		maxScore *= Mathf.Lerp(0.8f, 1.1f, tailScore * tailScore);
		maxScore *= Mathf.Lerp(0.05f, 1.0f, headScore * headScore);
		maxScore *= Mathf.Lerp(0.2f, 1.0f, bodyScore * bodyScore);
		return Mathf.Round(maxScore);
	}
}
