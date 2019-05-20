using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	public bool gameOver = false;
	public GameObject bird;
	public int score = 0;
	void Update ()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		if (gameOver)
			return ;
		if (Input.GetKey(KeyCode.Space))
				bird.transform.Translate(0, 0.2f, 0);
		else
			bird.transform.Translate(0, -0.1f, 0);
	}

	void OnCollisionEnter2D(Collision2D col)
    {
		gameOver = true;
    }
}
