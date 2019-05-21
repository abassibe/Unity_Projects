using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	private bool isActive = false;

	private int maxJump = 1;

	private Vector3 originalPos;

	void Start ()
	{
		originalPos = gameObject.transform.localPosition;
		if (gameObject.name == "red")
			isActive = true;
	}
	
	void FixedUpdate ()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		if (Input.GetKey(KeyCode.R))
			gameObject.transform.localPosition = originalPos;
		if (Input.GetKey(KeyCode.Alpha1))
		{
			if (gameObject.name == "red")
				isActive = true;
			else
				isActive = false;
		}
		if (Input.GetKey(KeyCode.Alpha2))
		{
			if (gameObject.name == "yellow")
				isActive = true;
			else
				isActive = false;
		}
		if (Input.GetKey(KeyCode.Alpha3))
		{
			if (gameObject.name == "blue")
				isActive = true;
			else
				isActive = false;
		}
		if (isActive)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.1f, 0), ForceMode2D.Impulse);
			if (Input.GetKey(KeyCode.RightArrow))
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0.1f, 0), ForceMode2D.Impulse);
			if (Input.GetKeyDown(KeyCode.Space) && maxJump > 0)
			{
				maxJump = 0;
				if (gameObject.name == "blue")
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse);
				if (gameObject.name == "yellow")
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4f), ForceMode2D.Impulse);
				if (gameObject.name == "red")
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) 
 	{
		maxJump = 1;
 	}
}
