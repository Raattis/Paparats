using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    void Awake()
    {
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

        GameObject.DontDestroyOnLoad(gameObject);   
    }

    void Update()
    {
        
    }
}
