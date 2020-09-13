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
    static int scaleFont(int orig)
    {
        return Mathf.RoundToInt(orig * Mathf.Lerp(-0.05f, 0.8f, Camera.main.pixelHeight / 1300.0f));
    }

    void OnGUI()
    {
        if (!ended)
            return;

        Color textColor = new Color(1.0f, 1.0f, 0.2f);

        Rect pixelRect = Camera.main.pixelRect;
        pixelRect.y -= 50;
        Bgm.shadedText(pixelRect, "Money money money " + finalScore + "€", 80, textColor);

        if (endTime + restartDuration < Time.time)
        {
            pixelRect.y += 100;
            Bgm.shadedText(pixelRect, "Press any button to restart", 80, textColor);
        }
    }
}
