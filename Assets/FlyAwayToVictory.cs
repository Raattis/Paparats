using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAwayToVictory : MonoBehaviour
{
    bool started = false;
    Vector3 originalPos;
    float startTime = 0.0f;

    void Update()
    {
        FinalScore finalScore = FindObjectOfType<FinalScore>();
        if (finalScore == null || !finalScore.isGameOver)
            return;

        if (!started)
        {
            started = true;
            originalPos = transform.localPosition;
            startTime = Time.time;
        }

        float t = Time.time - startTime;

        float x = Mathf.Cos(t * 2.0f);
        float y = t * 0.3f + Mathf.Sin(t * 4.0f) * Mathf.Lerp(0.8f, 0.0f, Mathf.Clamp01(t * 0.3f - 0.5f));
        transform.localPosition = originalPos + new Vector3(x, y, 0) * t;
    }
}
