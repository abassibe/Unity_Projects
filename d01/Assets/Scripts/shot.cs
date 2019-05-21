using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shot : MonoBehaviour
{

	public float speed;

	public bool toLeft;

	void Update ()
	{
		if (toLeft)
			transform.Translate(new Vector3(-speed, 0, 0));
		else
			transform.Translate(new Vector3(speed, 0, 0));
	}

	void OnCollisionEnter2D(Collision2D collision)
 	{
		if ((collision.gameObject.name == "red" && gameObject.name.Contains("redShot")) || (gameObject.name.Contains("redShot") && collision.gameObject.name == "Wall3"))
            GameObject.Destroy(gameObject);
		if ((collision.gameObject.name == "yellow" && gameObject.name.Contains("yellowShot")) || (gameObject.name.Contains("yellowShot") && collision.gameObject.name == "Wall2"))
            GameObject.Destroy(gameObject);
 	}
}
