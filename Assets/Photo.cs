using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FloatingScore
{
    public string text;
    public Vector3 pos;

    public FloatingScore(string text, Vector3 pos)
    {
        this.text = text;
        this.pos = pos;
    }
}

public class Photo : MonoBehaviour
{
    float reloadTimer = 0.0f;

    List<FloatingScore> floatingScores = new List<FloatingScore>();

    void Start()
    {
        
    }

    void Update()
    {
        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && reloadTimer <= 0.0f)
        {
            TakePhoto();
            reloadTimer = 1.0f;

            Celeb celeb = GameObject.FindObjectOfType<Celeb>();
            floatingScores.Add(new FloatingScore("" + celeb.GetScore() + "€", celeb.transform.position + new Vector3(0.0f, 1.0f, 0.0f)));
        }

        for (int i = floatingScores.Count; i-- > 0; )
        {
            FloatingScore floatingScore = floatingScores[i];
            floatingScore.pos.y += Time.deltaTime * 1.0f;
            floatingScores[i] = floatingScore;
            if (floatingScore.pos.y > 20.0f)
                floatingScores.RemoveAt(i);
        }
    }

    void TakePhoto()
    {
        GameObject.FindObjectOfType<Flash>().flash();
    }

    private void OnGUI()
    {
        foreach (FloatingScore floatingScore in floatingScores)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector2(floatingScore.pos.x, -floatingScore.pos.y));
            GUIStyle style = GUIStyle.none;
            style.normal.textColor = new Color(0.8f, 0.3f, 0.3f);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(new Rect(screenPoint.x - 500.0f, screenPoint.y - 50.0f, 1000.0f, 100.0f), floatingScore.text, style);
        }
    }
}
