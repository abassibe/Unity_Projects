using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour {

	List<Vector2> randomCoo;

	void Start()
	{
		randomCoo = new List<Vector2>();
		randomCoo.Add(new Vector2(-6.33f, -7.417f));
		randomCoo.Add(new Vector2(-0.6f, -7.417f));
		randomCoo.Add(new Vector2(-3.396f, -7.417f));
		randomCoo.Add(new Vector2(-0.6f, -3.9f));
		randomCoo.Add(new Vector2(-6.42f, -3.9f));
		randomCoo.Add(new Vector2(-3.47f, -3.9f));
		randomCoo.Add(new Vector2(5.866f, 0.104f));
	}

	void OnTriggerEnter2D(Collider2D obj)
	{
		int r = Random.Range(0, randomCoo.Count);
		switch (gameObject.tag)
		{
			case "Teleport":
				obj.transform.position = new Vector2(-7.35f, 5.893f);
				break;
			case "TeleportMaze":
				obj.transform.position = randomCoo[r];
				break;
		}
	}
}
