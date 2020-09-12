using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebBoundingBox : MonoBehaviour
{
	public Vector2 pos { get { return new Vector2(transform.position.x, transform.position.y) - size * 0.5f; } }
	public Vector2 size { get { return new Vector2(transform.lossyScale.x, transform.lossyScale.y) * 0.136f; } }

	public Box boundingBox { get { return new Box(pos, size); } }

	Vector2 originalPosition;

	Box biggestBox;
	float visibleArea;

	public enum BodyPart {
		Head,
		Body,
		Tail,
		Feet
	};

	public BodyPart bodyPart;

	void Start()
	{
		GetComponent<SpriteRenderer>().enabled = false;

	}

	// Update is called once per frame
	void Update()
	{
		UpdateVisibility();
	}

	public void UpdateVisibility()
	{
		List<BoundingBox> holes = new List<BoundingBox>();
		List<BoundingBox> obstructions = new List<BoundingBox>();
		foreach (BoundingBox box in GameObject.FindObjectsOfType<BoundingBox>())
		{
			if (box.obstruction)
				obstructions.Add(box);
			else
				holes.Add(box);
		}

		float smallestHoleArea = 1.0f;
		Box smallestHole = boundingBox;
		float biggestObstructionArea = 0.0f;
		Box biggestObstruction = boundingBox;
		float visibleArea = 0.0f;
		foreach (BoundingBox hole in holes)
		{
			if (!hole.enabled)
				continue;

			Box visibleThroughHole = boundingBox.clip(hole.boundingBox);
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
			this.biggestBox = biggestObstruction;
		else if (smallestHoleArea < 1.0f)
			this.biggestBox = smallestHole;
		else
			this.biggestBox = boundingBox;

		this.visibleArea = visibleArea;
	}

	private void OnGUI()
	{
		biggestBox.onGuiBox("Score: " + Mathf.Round(visibleArea * 100.0f));
		GUIUtility.ExitGUI();
	}
}
