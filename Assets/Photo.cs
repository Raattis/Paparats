using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct FloatingScore
{
    public float score;
    public Vector3 pos;

    public FloatingScore(float text, Vector3 pos)
    {
        this.score = text;
        this.pos = pos;
    }
}

public class Photo : MonoBehaviour
{
    public AudioClip shutter;
    public AudioClip reload;

    float reloadTimer = 0.0f;

    List<FloatingScore> floatingScores = new List<FloatingScore>();

    float money = 0.0f;
    int filmLeft = 16;

    void Start()
    {
        
    }

    void Update()
    {
        float reloadDuration = 1.0f;

        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && reloadTimer <= 0.0f && filmLeft > 0)
        {
            TakePhoto();
            reloadTimer = reloadDuration;

            Celeb celeb = GameObject.FindObjectOfType<Celeb>();
            float score = celeb.GetScore();
            floatingScores.Add(new FloatingScore(score, celeb.transform.position + new Vector3(0.0f, 1.0f, 0.0f)));

            money += score;
            filmLeft -= 1;

            GetComponent<AudioSource>().PlayOneShot(shutter);
        }

        if (reloadTimer + Time.deltaTime >= reloadDuration * 0.7f && reloadTimer < reloadDuration * 0.7f)
        {
            GetComponent<AudioSource>().PlayOneShot(reload);
        }

        for (int i = floatingScores.Count; i-- > 0; )
        {
            FloatingScore floatingScore = floatingScores[i];
            floatingScore.pos.y += Time.deltaTime * 3.0f;
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
            style.fontSize = Mathf.RoundToInt(Mathf.Lerp(20.0f, 50.0f, Mathf.Clamp01((floatingScore.score - 10.0f) / 60.0f)));
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(new Rect(screenPoint.x - 500.0f, screenPoint.y - 50.0f, 1000.0f, 100.0f), "" + floatingScore.score + "€", style);
        }

        {
            GUIStyle style = GUIStyle.none;
            style.normal.textColor = new Color(1.0f, 1.0f, 0.2f);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 40;
            style.alignment = TextAnchor.MiddleCenter;

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, -transform.position.y));
            GUI.Label(new Rect(screenPoint.x - 500.0f - 100.0f, screenPoint.y - 100.0f, 1000.0f, 100.0f), "" + money + "€", style);
            GUI.Label(new Rect(screenPoint.x - 500.0f + 100.0f, screenPoint.y - 100.0f, 1000.0f, 100.0f), "" + filmLeft, style);
        }
    }
}
