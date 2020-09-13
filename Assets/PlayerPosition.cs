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

    void Start()
    {
        
    }

    void Update()
    {
        FinalScore finalScore = FindObjectOfType<FinalScore>();
        if (finalScore == null || finalScore.isGameOver)
            return;

        x += Time.deltaTime * speed.x * Input.GetAxis("Horizontal");
        y += Time.deltaTime * speed.y * Input.GetAxis("Vertical");

		x = Mathf.Clamp(x, minX, maxX);
		y = Mathf.Clamp(y, minY, maxY);
	}
}
