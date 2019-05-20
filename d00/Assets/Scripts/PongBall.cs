using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
	public GameObject ball;

	private float xDirection;

	private float yDirection;

	private int p1Score = 0;

	private int p2Score = 0;

	void Start()
	{
		xDirection = Random.Range(-0.09f, 0.09f);
		yDirection = Random.Range(-0.09f, 0.09f);
		ball.transform.Translate(xDirection, yDirection, 0);
	}

	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		ball.transform.Translate(xDirection, yDirection, 0);
		if (ball.transform.localPosition.x < -5)
		{
			p2Score++;
			xDirection = Random.Range(-0.09f, 0.09f);
			yDirection = Random.Range(-0.09f, 0.09f);
			ball.transform.Translate(ball.transform.localPosition.x * -1, ball.transform.localPosition.y * -1, 0);
			Debug.Log(" Player 1: " + p1Score + " | Player 2: " + p2Score);
		}
		else if (ball.transform.localPosition.x > 5)
		{
			p1Score++;
			xDirection = Random.Range(-0.09f, 0.09f);
			yDirection = Random.Range(-0.09f, 0.09f);
			ball.transform.Translate(ball.transform.localPosition.x * -1, ball.transform.localPosition.y * -1, 0);
			Debug.Log(" Player 1: " + p1Score + " | Player 2: " + p2Score);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.name == "WallUp" || col.gameObject.name == "WallDown")
			yDirection *= -1;
		else
		{
			xDirection *= -1;
			xDirection /= 0.9f;
			yDirection /= 0.9f;
		}
    }
}
