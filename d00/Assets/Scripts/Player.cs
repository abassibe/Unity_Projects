using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject player;

	void FixedUpdate ()
	{
		switch (player.name)
		{
			case "Player1":
				if (Input.GetKey(KeyCode.W))
					player.transform.Translate(0, 0.1f, 0);
				if (Input.GetKey(KeyCode.S))
					player.transform.Translate(0, -0.1f, 0);
				break;
			case "Player2":
				if (Input.GetKey(KeyCode.UpArrow))
					player.transform.Translate(0, 0.1f, 0);
				if (Input.GetKey(KeyCode.DownArrow))
					player.transform.Translate(0, -0.1f, 0);
				break;
		}
	}
}
