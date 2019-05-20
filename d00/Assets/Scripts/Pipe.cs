using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
	public GameObject pipe;
	
	public Bird bird;

	private int autorisation = 0;

	private float moveSpeed = -0.1f;
	void Update ()
	{
		if (bird.gameOver)
			return;
		if (pipe.transform.localPosition.x > -6)
			pipe.transform.Translate(moveSpeed, 0, 0);
		else if (pipe.transform.localPosition.x <= -6 && autorisation >= 100)
		{
			pipe.transform.Translate((pipe.transform.localPosition.x * - 1) + 7, 0, 0);
			bird.score += 5;
			Debug.Log("Score: " + bird.score);
			Debug.Log("Time: " + Mathf.RoundToInt(Time.time) + "s");
			autorisation = bird.score * 2;
			moveSpeed += -0.05f;
		}
		else
			autorisation += (bird.score % 5) + 1;
	}

	void OnCollisionEnter2D(Collision2D col)
    {
		Debug.Log("Score: " + bird.score);
		Debug.Log("Time: " + Mathf.RoundToInt(Time.time) + "s");
		bird.gameOver = true;
    }
}
