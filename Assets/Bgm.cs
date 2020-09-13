using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    static float levelStartTime = 0.0f;

    void Awake()
    {
        levelStartTime = Time.time + 0.5f;

        bool notAlone = false;
        foreach (Bgm bgm in FindObjectsOfType<Bgm>())
        {
            if (bgm != this)
            {
                notAlone = true;
                break;
            }
        }

        if (notAlone)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        Application.targetFrameRate = 60;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Application.loadedLevel == 0)
        {
            if (Input.anyKeyDown && Time.time > levelStartTime)
            {
                Application.LoadLevel(1);
            }
        }
    }

    static public void shadedText(Rect rect, string text, int fontHeight, Color color)
    {
        GUIStyle style = GUIStyle.none;
        float alpha = Mathf.Clamp01((levelStartTime + 3.0f - Time.time) / 1.0f);
        style.normal.textColor = new Color(0.8f, 0.7f, 1.0f, alpha);
        style.fontStyle = FontStyle.Bold;
        style.fontSize = Photo.scaleFont(fontHeight);
        style.alignment = TextAnchor.MiddleCenter;

        style.normal.textColor = new Color(0, 0, 0, color.a * color.a);
        rect.x -= 1;
        rect.y -= 1;
        GUI.Label(rect, text, style);
        rect.x += 2;
        GUI.Label(rect, text, style);
        rect.y += 2;
        GUI.Label(rect, text, style);
        rect.x -= 2;
        GUI.Label(rect, text, style);
        rect.x += 1;
        rect.y -= 1;
        style.normal.textColor = color;
        GUI.Label(rect, text, style);
    }

    private void OnGUI()
    {
        if (Application.loadedLevel == 0)
        {
            Rect pixelRect = Camera.main.pixelRect;
            pixelRect.height *= 0.2f;
            shadedText(pixelRect, "Paparats", 300, new Color(0.8f, 0.7f, 1.0f, 0.8f));

            pixelRect = Camera.main.pixelRect;
            pixelRect.y = Camera.main.pixelRect.height * 0.69f;
            pixelRect.height = Camera.main.pixelRect.height * 0.30f;
            float width = Camera.main.pixelRect.height * 0.1f;
            pixelRect.x += (Camera.main.pixelRect.width - width) * 0.44f;
            pixelRect.width = width * 0.5f;
            shadedText(pixelRect, "You are a paparazzi rat\non a mission. Take good\nphotos of the Celebcat.\nThe better the photos\nthe more you earn.", 100, new Color(0.8f, 0.7f, 1.0f, 0.8f));
        }
        else if (Application.loadedLevel == 1 && levelStartTime + 10.0f > Time.time)
        {
            Rect pixelRect = Camera.main.pixelRect;
            pixelRect.y = Camera.main.pixelRect.height * 0.4f;
            pixelRect.height *= 0.2f;
            float alpha = Mathf.Clamp01((levelStartTime + 7.0f - Time.time) / 1.0f);

            if (!Application.isMobilePlatform)
                shadedText(pixelRect, "Fly with W,A,S,D or by dragging with mouse.\nTake photos with Space or mouse click.", 100, new Color(0.8f, 0.7f, 1.0f, alpha));
            else
                shadedText(pixelRect, "Fly by dragging.\nTap to take photos.", 100, new Color(0.8f, 0.7f, 1.0f, alpha));

        }
    }
}
