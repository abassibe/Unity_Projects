using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public bool isMoving = false;
	public bool gameOver = false;

	public int score = -15;

	private float Strength;

	public float strength
	{
		get
		{
			return Strength;
		}
		set
		{
			if (value < 0)
				value *= -1;
			Strength = Mathf.Clamp(value, 0f, 1.0f);
			if (Strength > 0)
			isMoving = true;
		}
	}

	public GameObject ball;

	private bool toUp = true;

	void Update ()
	{
		if (gameOver)
			return;
		if (Strength < 0.05 && (ball.transform.localPosition.y > 2.8 && ball.transform.localPosition.y < 3.2))
		{
			float finish = 3 - ball.transform.localPosition.y;
			if (ball.transform.localPosition.y > 3)
				finish *= -1;
			ball.transform.Translate(0, finish, 0);
			Debug.Log("Final score = " + score);
			gameOver = true;
			return;
		}
		if (Strength > 0)
		{
			if (!toUp)
				ball.transform.Translate(0, -Strength, 0);
			else
				ball.transform.Translate(0, Strength, 0);
			if (ball.transform.localPosition.y >= 5)
				toUp = false;
			else if (ball.transform.localPosition.y <= -5)
				toUp = true;
			Strength = Strength / 1.05f;
			if (Strength <= 0.01)
			{
				Strength = 0;
				isMoving = false;
				toUp = true;
				score += 5;
			}
		}
	}
}
