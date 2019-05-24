using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
	public int lifePoint;

	public player Player;
	public orc Orc;

	public heroesManager hm;

	float lastSpawn = 0.0f;

	public float spawnTimer = 10.0f;

	void Update ()
	{
		if (Time.time - lastSpawn > spawnTimer)
		{
			if (gameObject.name == "humanTownCenter")
			{
				player tmp = GameObject.Instantiate(Player, new Vector3(-3.26f, 2.86f, 0), Quaternion.identity);
				tmp.isSelected += hm.addPlayer;
				hm.players.Add(tmp);
			}
			else if (gameObject.name == "orcTownCenter")
				GameObject.Instantiate(Orc, new Vector3(5.29f, -1.72f, 0), Quaternion.identity);
			lastSpawn = Time.time;
		}
		if (lifePoint <= 0)
		{
			GameObject.Destroy(this.gameObject);
			if (gameObject.name == "orcTownCenter")
				print("The human team win!");
			if (gameObject.name == "humanTownCenter")
				print("The orcs team win!");
			else if (gameObject.tag == "buildingOrc")
			{
				building orcb = GameObject.Find("orcTownCenter").GetComponent<building>();
				orcb.spawnTimer += 2.5f;
			}
			else if (gameObject.tag == "buildingHuman")
			{
				building orcb = GameObject.Find("humanTownCenter").GetComponent<building>();
				orcb.spawnTimer += 2.5f;
			}
		}
	}
}
