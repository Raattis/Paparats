using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
	class BirdState
	{
		public Transform trans = null;
		public Vector2 offset = new Vector2();
		public Vector2 originalPosition;
	}

	List<BirdState> birds = new List<BirdState>();

	float groundDuration = 5f;
	float riseDuration = 0.75f;
	float flyDuration = 5f;
	float landDuration = 2f;

	float stateStartedTime = 0;
	enum FlockState
	{
		ground, rise, fly, land
	}
	FlockState state = FlockState.ground;
	FlockState nextState = FlockState.ground;

	// Start is called before the first frame update
	void Awake()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			BirdState bird = new BirdState();
			bird.trans = transform.GetChild(i);
			bird.originalPosition = bird.trans.localPosition;

			birds.Add(bird);
		}
	}

	void Start()
	{
		float t = Time.time;
		stateStartedTime = t - Random.value * 5f;

	}

	// Update is called once per frame
	void Update()
	{
		float t = getTime();
		
		bool moveToState = false;
		if (state != nextState)
		{
			stateStartedTime = t;
			state = nextState;
			moveToState = true;
		}
		
		float stateElapsed = t - stateStartedTime;

		switch (state)
		{
			case FlockState.ground:
			{
				foreach (var bird in birds)
				{
					bird.offset = Vector2.zero;
					bird.trans.localPosition = bird.originalPosition;

					bird.trans.GetComponent<Animator>().SetInteger("animState", 0);
				}

				if (stateElapsed > groundDuration)
					nextState = FlockState.rise;
			}
			break;

			case FlockState.rise:
			{
				foreach (var bird in birds)
				{
					bird.offset += new Vector2(0, Time.deltaTime * 4f);
					bird.trans.localPosition = bird.originalPosition + bird.offset;

					bird.trans.GetComponent<Animator>().SetInteger("animState", 1);
				}

				if (stateElapsed > riseDuration)
					nextState = FlockState.fly;
			}
			break;

			case FlockState.fly:
			{
				for (int i = 0; i < birds.Count; ++i)
				{
					BirdState bird = birds[i];

					bird.offset += new Vector2(Mathf.Sin(t * 4 + i) * 0.018f, Mathf.Sin(t * 2.4f + i) * 0.04f);

					bird.trans.localPosition = bird.originalPosition + bird.offset;
				}

				if (stateElapsed > flyDuration)
					nextState = FlockState.land;
			}
			break;

			case FlockState.land:
			{
				foreach (var bird in birds)
				{
					bird.offset -= bird.offset * landDuration * Time.deltaTime;

					bird.trans.localPosition = bird.originalPosition + bird.offset;
				}

				if (stateElapsed > landDuration)
					nextState = FlockState.ground;
			}
			break;
		}
	}
		
	void OnGUI()
	{
		//GUI.Label(new Rect(200, 10, 500, 50), $"State elapsed: {getTime() - stateStartedTime}, state: {state.ToString()}, bird count {birds.Count}");
	}

	float getTime()
	{
		return Time.time;
	}
}

