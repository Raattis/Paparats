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

    public Texture moneyTexture;
    public Texture filmTexture;

    float reloadTimer = 0.0f;

    List<FloatingScore> floatingScores = new List<FloatingScore>();

    float money = 0.0f;
    int filmLeft = 16;

    void Start()
    {
        
    }

    float reloadDuration = 1.0f;
    void Update()
    {
        FinalScore finalScore = FindObjectOfType<FinalScore>();
        if (finalScore == null || finalScore.isGameOver)
            return;

        if (reloadTimer > 0)
            reloadTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && reloadTimer <= 0.0f && filmLeft > 0)
        {
            TakePhotoImpl();
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

    public void TakePhotoImpl()
    {
        reloadTimer = reloadDuration;

        Celeb celeb = GameObject.FindObjectOfType<Celeb>();
        float score = celeb.GetScore();
        floatingScores.Add(new FloatingScore(score, celeb.transform.position + new Vector3(0.0f, 1.0f, 0.0f)));

        money += score;
        filmLeft -= 1;

        GetComponent<AudioSource>().PlayOneShot(shutter);

        GameObject.FindObjectOfType<Flash>().flash();

        if (filmLeft <= 0)
            FindObjectOfType<FinalScore>().GameOver(money);
    }

    static public void TakePhoto()
    {
        FindObjectOfType<Photo>().TakePhotoImpl();
    }

    static public int scaleFont(int orig)
    {
        return Mathf.RoundToInt(orig * Mathf.Lerp(-0.05f, 0.8f, Camera.main.pixelHeight / 1300.0f));
    }

    private void OnGUI()
    {
        FinalScore finalScore = FindObjectOfType<FinalScore>();
        if (finalScore == null || finalScore.isGameOver)
            return;

        foreach (FloatingScore floatingScore in floatingScores)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector2(floatingScore.pos.x, -floatingScore.pos.y));
            Color textColor = new Color(1.0f, 0.9f, 0.3f);
            int fontHeight = Mathf.RoundToInt(Mathf.Lerp(60.0f, 100.0f, Mathf.Clamp01((floatingScore.score - 10.0f) / 60.0f)));
            Bgm.shadedText(new Rect(screenPoint.x - 500.0f, screenPoint.y - 50.0f, 1000.0f, 100.0f), "" + floatingScore.score + "€", fontHeight, textColor);
        }

        {
            GUIStyle style = GUIStyle.none;
            style.normal.textColor = new Color(1.0f, 1.0f, 0.5f, 0.8f);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 45;
            style.fontSize = scaleFont(style.fontSize);
            style.alignment = TextAnchor.MiddleCenter;

            Rect pixelRect = Camera.main.pixelRect;
            Rect iconRect = pixelRect;
            iconRect.height *= 0.1f;
            iconRect.width = iconRect.height;
            iconRect.x = pixelRect.width * 0.5f - iconRect.width;
            iconRect.y += pixelRect.height * 0.02f;
            GUI.DrawTexture(iconRect, moneyTexture);

            Rect textRect = iconRect;
            textRect.y -= iconRect.height * 0.02f;
            Bgm.shadedText(textRect, "" + money, 45, style.normal.textColor);

            style.normal.textColor = new Color(0.0f, 0.0f, 0.0f, 0.5f);
            iconRect.x += iconRect.width;
            GUI.DrawTexture(iconRect, filmTexture);

            style.fontSize = 60;
            style.fontSize = scaleFont(style.fontSize);

            textRect = iconRect;
            textRect.y -= iconRect.height * 0.02f;
            textRect.x -= iconRect.height * 0.01f;
            GUI.Label(textRect, "" + filmLeft, style);
        }
    }
}
