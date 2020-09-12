using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public Vector2 min;
    public Vector2 max;

    public List<GameObject> cloudPrefabs;

    List<GameObject> spawned = new List<GameObject>();

    float spawnNext = 1.0f;


    void spawn(Vector2 pos)
    {
        GameObject g = GameObject.Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Count)]);
        g.transform.localPosition = pos;
        g.transform.localScale = g.transform.localScale * Random.Range(0.95f, 1.3f);
        g.transform.parent = transform;
        float gray = Random.Range(0.8f, 1.0f);
        float alpha = Random.Range(0.5f, 1.0f);
        g.GetComponent<SpriteRenderer>().color = new Color(gray, gray, gray, alpha);
        spawned.Add(g);
    }

    void Start()
    {
        for (int i = 0; i < 80; ++i)
        {
            spawn(new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)));
        }
    }

    void Update()
    {
        spawnNext -= Time.deltaTime;
        if (spawnNext < 0)
        {
            spawnNext = Random.Range(1.0f, 3.0f);
            spawn(new Vector2(max.x, Random.Range(min.y, max.y)));
        }

        for (int i = spawned.Count; i-- > 0;)
        {
            Vector3 p = spawned[i].transform.position;
            float speed = Mathf.Lerp(0.2f, 0.5f, (p.y - min.y) / (max.y - min.y));
            p.x -= Time.deltaTime * speed;
            spawned[i].transform.position = p;

            if (p.x < min.x)
            {
                GameObject.Destroy(spawned[i]);
                spawned.RemoveAt(i);
            }
        }
    }
}
