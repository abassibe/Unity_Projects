using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	public GameObject club;

	public Ball ball;

	private Vector3 origin;

	private bool waitingBall = false;

	void Start()
	{
		origin = new Vector3(-0.18f, ball.transform.localPosition.y + 0.32f, 0);
	}

	void Update ()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		if (ball.gameOver)
			return;
		if (ball.isMoving)
			return ;
		else if (waitingBall)
		{
			float newY = club.transform.localPosition.y - ball.transform.localPosition.y;
			club.transform.Translate(0, -newY + 0.32f, 0);
			origin = new Vector3(-0.18f, club.transform.localPosition.y, 0);
			waitingBall = false;
		}
		if (Input.GetKey(KeyCode.Space) && club.transform.localPosition.y > origin.y - 1)
		{
			club.transform.Translate(0, -0.05f, 0);
		}
		else if (!Input.GetKey(KeyCode.Space) && club.transform.localPosition.y != origin.y)
		{
			waitingBall = true;
			float newY = club.transform.localPosition.y -origin.y;
			ball.strength = newY;
			club.transform.Translate(0, -newY, 0);
			if (ball.score < 0)
				Debug.Log("Score: " + ball.score);
			else
				Debug.Log("Game Over");
		}
	}
}
