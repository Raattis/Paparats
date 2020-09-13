using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour
{
    float finalScore = 0.0f;
    float endTime = 0.0f;
    bool ended = false;
    float restartDuration = 2.0f;

    public bool isGameOver { get { return ended; } }

    public void GameOver(float finalScore)
    {
        this.finalScore = finalScore;
        endTime = Time.time;
        ended = true;
    }

    void Update()
    {
        if (!ended)
            return;

        if (Input.anyKeyDown && endTime + restartDuration < Time.time)
        {
            SceneManager.LoadScene(0);
        }
    }
    void OnGUI()
    {
        if (!ended)
            return;

        GUIStyle style = GUIStyle.none;
        style.normal.textColor = new Color(1.0f, 1.0f, 0.2f);
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;

        Rect pixelRect = Camera.main.pixelRect;
        pixelRect.y -= 50;
        GUI.Label(pixelRect, "Money money money " + finalScore + "€", style);

        if (endTime + restartDuration < Time.time)
        {
            pixelRect.y += 100;
            GUI.Label(pixelRect, "Press any button to restart", style);
        }
    }
}
