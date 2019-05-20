using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {

	public int breath = 0;
	public bool reBreath = false;
	public bool gameOver = false;
	private bool gameIsStarted = false;
	public GameObject BalloonGO;
	
	void Update () {
		if (!gameIsStarted)
		{
			if (Input.GetKeyDown(KeyCode.Space))
				gameIsStarted = true;
			else
				return ;
		}
		if (BalloonGO.transform.localScale.x >= 5)
		{
			GameObject.Destroy(BalloonGO);
			gameOver = true;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameObject.Destroy(BalloonGO);
        	Application.Quit();
		}
		if (gameOver)
			return ;
		if (reBreath)
		{
			breath -= 1;
			if (BalloonGO.transform.localScale.x > 0.1f && BalloonGO.transform.localScale.y > 0.1f)
				BalloonGO.transform.localScale -= new Vector3(0.1f, 0.1f, 0);
			if (BalloonGO.transform.localScale.x <= 0.1f || BalloonGO.transform.localScale.y <= 0.1f)
			{
				Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time) + "s");
				gameOver = true;
			}
			if (breath <= 0)
				reBreath = false;
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			BalloonGO.transform.localScale += new Vector3(1, 1, 0);
			breath += 15;
			if (breath >= 100)
				reBreath = true;
		}
		else if (BalloonGO.transform.localScale.x > 0.1f && BalloonGO.transform.localScale.y > 0.1f)
		{
			breath -= 1;
			BalloonGO.transform.localScale -= new Vector3(0.1f, 0.1f, 0);
			if (BalloonGO.transform.localScale.x <= 0.1f || BalloonGO.transform.localScale.y <= 0.1f)
			{
				Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time) + "s");
				gameOver = true;
			}
		}
	}
}
