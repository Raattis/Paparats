using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kulkue : MonoBehaviour
{
	[SerializeField]
	List<GameObject> kulkijaPool;

	[SerializeField]
	Transform walkArea;

	class KulkijaState
	{
		public Transform trans;
		public float walkDir = 1;
		public float speed = 5;
	}
	List<KulkijaState> activeKulkijas = new List<KulkijaState>();


	const float spawnInterval = 0.2f;
	float lastSpawnTime = 0;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float t = Time.time;

		if (t > lastSpawnTime + spawnInterval)
		{
			GameObject kulkijaPrefab = kulkijaPool[(int)(Random.value * kulkijaPool.Count)];

			GameObject go = GameObject.Instantiate<GameObject>(kulkijaPrefab, transform, false);

			KulkijaState kulkija = new KulkijaState();
			kulkija.trans = go.transform;
			kulkija.walkDir = Random.value < 0.5 ? -1 : 1;
			kulkija.speed = Random.value * 2 + 5f;

			kulkija.trans.localPosition = new Vector3(walkArea.localScale.x / 2 * -1 * kulkija.walkDir, 0, 0);

			activeKulkijas.Add(kulkija);

			lastSpawnTime = t;
		}

		for (int i = activeKulkijas.Count; i-- > 0;)
		{
			KulkijaState kulkija = activeKulkijas[i];

			kulkija.trans.localPosition += new Vector3(kulkija.walkDir * Time.deltaTime * kulkija.speed, 0, 0);

			if (kulkija.trans.localPosition.x * kulkija.walkDir > walkArea.localScale.x / 2f)
			{
				// Exiting walk area. Despawn.
				GameObject.Destroy(kulkija.trans.gameObject);
				activeKulkijas.RemoveAt(i);
				continue;
			}
		}
	}
}
