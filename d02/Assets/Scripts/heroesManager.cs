using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroesManager : MonoBehaviour
{
	public List<player> players = new List<player>();

	private List<player> selectedPlayers = new List<player>();

	void Start()
	{
		foreach (player p in players)
			p.isSelected += addPlayer;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown(1))
			selectedPlayers.Clear();
		if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftControl))
		{
			Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			destination.z = 0;
			foreach (player p in selectedPlayers)
			{
				if ((destination.x >= (p.transform.localPosition.x - (0.484375f / 2)) && destination.x <= (p.transform.localPosition.x + (0.484375f / 2))) &&
					(destination.y >= (p.transform.localPosition.y - (0.625f / 2 )) && destination.y <= (p.transform.localPosition.y + (0.625f / 2))))
					continue;
				p.Destination = destination;
				destination.x -= 0.5f;
			}
		}
	}

	public void addPlayer(player p)
	{
		if (p == null)
			return;
		if (Input.GetKey(KeyCode.LeftControl))
			selectedPlayers.Add(p);
		else
		{
			selectedPlayers.Clear();
			selectedPlayers.Add(p);
		}
	}
}
