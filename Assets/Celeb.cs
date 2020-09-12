using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Celeb : MonoBehaviour
{
	public Vector2 pos { get { return new Vector2(transform.position.x, transform.position.y) - size * 0.5f; } }
	public Vector2 size { get { return new Vector2(transform.lossyScale.x, transform.lossyScale.y) * 0.136f; } }

	public Rect boundingBox { get { return new Rect(pos, size); } }

	List<BoundingBox> holes = new List<BoundingBox>();
	List<BoundingBox> obstructions = new List<BoundingBox>();

	Vector2 originalPosition;

	Rect biggestRect;
	float visibleArea;


	void Start()
    {
		originalPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);

		
		foreach (BoundingBox box in GameObject.FindObjectsOfType<BoundingBox>())
        {
			if (box.obstruction)
				obstructions.Add(box);
			else
				holes.Add(box);
		}
	}

    // Update is called once per frame
    void Update()
    {
		transform.localPosition = originalPosition + new Vector2(Mathf.Sin(Time.time), Mathf.Cos(Time.time));
		UpdateVisibility();
	}

	public void UpdateVisibility()
	{
		float smallestHoleArea = 1.0f;
		Rect smallestHole = boundingBox;
		float biggestObstructionArea = 0.0f;
		Rect biggestObstruction = boundingBox;
		float visibleArea = 0.0f;
		foreach (BoundingBox hole in holes)
		{
			if (!hole.enabled)
				continue;

			Rect visibleThroughHole = boundingBox.clip(hole.boundingBox);
			float visibleThroughHoleArea = boundingBox.overlap(hole.boundingBox);
			if (smallestHoleArea > visibleThroughHoleArea && visibleThroughHoleArea > 0.0f)
			{
				smallestHoleArea = visibleThroughHoleArea;
				smallestHole = visibleThroughHole;
			}

			float obstructedArea = 0.0f;
			foreach (BoundingBox obstruction in obstructions)
			{
				if (!obstruction.enabled)
					continue;

				float area = visibleThroughHoleArea * visibleThroughHole.overlap(obstruction.boundingBox);
				if (area > 0.0f)
				{
					obstruction.boundingBox.clip(visibleThroughHole).draw(Color.red);

					if (biggestObstructionArea < area)
					{
						biggestObstructionArea = area;
						biggestObstruction = obstruction.boundingBox.clip(visibleThroughHole);
					}
				}

				obstructedArea += area;
			}

			visibleArea += Mathf.Clamp01(visibleThroughHoleArea - obstructedArea);
		}

		if (biggestObstructionArea > 0.0f)
			this.biggestRect = biggestObstruction;
		else if (smallestHoleArea < 1.0f)
			this.biggestRect = smallestHole;
		else
			this.biggestRect = boundingBox;

		this.visibleArea = visibleArea;
	}

	private void OnGUI()
	{
		biggestRect.onGuiBox("Score: " + Mathf.Round(visibleArea * 100.0f));
	}
}
