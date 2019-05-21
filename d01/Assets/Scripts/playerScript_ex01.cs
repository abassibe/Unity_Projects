using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex01 : MonoBehaviour {

	private bool isActive = false;

	private int maxJump = 1;

	private Vector3 originalPos;

	void Start ()
	{
		originalPos = gameObject.transform.localPosition;
		if (gameObject.name == "red")
			isActive = true;
		else
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
			{
                GetComponent<Rigidbody2D>().mass = 1;
				isActive = true;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			else
			{
				isActive = false;
                GetComponent<Rigidbody2D>().mass = 500;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
		}
		if (Input.GetKey(KeyCode.Alpha2))
		{
			if (gameObject.name == "yellow")
			{
                GetComponent<Rigidbody2D>().mass = 1;
				isActive = true;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			else
			{
				isActive = false;
                GetComponent<Rigidbody2D>().mass = 500;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
		}
		if (Input.GetKey(KeyCode.Alpha3))
		{
			if (gameObject.name == "blue")
			{
                GetComponent<Rigidbody2D>().mass = 1;
				isActive = true;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			else
			{
				isActive = false;
                GetComponent<Rigidbody2D>().mass = 500;
				GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
		}
		if (isActive)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				if (gameObject.name == "blue")
					gameObject.transform.Translate(-0.01f, 0, 0);
				if (gameObject.name == "yellow")
					gameObject.transform.Translate(-0.05f, 0, 0);
				if (gameObject.name == "red")
					gameObject.transform.Translate(-0.03f, 0, 0);
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				if (gameObject.name == "blue")
					gameObject.transform.Translate(0.01f, 0, 0);
				if (gameObject.name == "yellow")
					gameObject.transform.Translate(0.05f, 0, 0);
				if (gameObject.name == "red")
					gameObject.transform.Translate(0.03f, 0, 0);
			}
			if (Input.GetKeyDown(KeyCode.Space) && maxJump > 0)
			{
				maxJump = 0;
				if (gameObject.name == "blue")
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse);
				if (gameObject.name == "yellow")
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
				if (gameObject.name == "red")
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4f), ForceMode2D.Impulse);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision) 
 	{
		maxJump = 1;
 	}
}
