using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    static float startTime = 0.0f;

    void Awake()
    {
        startTime = Time.time + 0.5f;

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
            if (Input.anyKeyDown && Time.time > startTime)
            {
                Application.LoadLevel(1);
            }
        }
    }

    private void OnGUI()
    {
        if (Application.loadedLevel == 0)
        {
            GUIStyle style = GUIStyle.none;
            style.normal.textColor = new Color(0.8f, 0.7f, 1.0f, 0.8f);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 300;
            style.fontSize = Photo.scaleFont(style.fontSize);
            style.alignment = TextAnchor.MiddleCenter;

            Rect pixelRect = Camera.main.pixelRect;
            pixelRect.height *= 0.2f;
            GUI.Label(pixelRect, "Paparats", style);

            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 80;
            style.fontSize = Photo.scaleFont(style.fontSize);

            pixelRect = Camera.main.pixelRect;
            pixelRect.y = Camera.main.pixelRect.height * 0.69f;
            pixelRect.height = Camera.main.pixelRect.height * 0.30f;
            float width = Camera.main.pixelRect.height * 0.1f;
            pixelRect.x += (Camera.main.pixelRect.width - width) * 0.44f;
            pixelRect.width = width * 0.5f;
            GUI.Label(pixelRect, "You are a paparazzi rat\non a mission. Take good\nphotos of the Celebcat.\nThe better the photos\nthe more you earn.", style);
        }
    }
}
