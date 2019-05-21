using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public GameObject actualPlayer;

	private Vector3 offset;

	void Start () 
	{
		offset = transform.position - actualPlayer.transform.position;
	}
	
	void LateUpdate ()
	{
		if (Input.GetKey(KeyCode.Alpha1))
			actualPlayer = GameObject.Find("red");
		else if (Input.GetKey(KeyCode.Alpha2))
			actualPlayer = GameObject.Find("yellow");
		else if (Input.GetKey(KeyCode.Alpha3))
			actualPlayer = GameObject.Find("blue");
		if (actualPlayer.transform.localPosition.y > 0)
			transform.position = actualPlayer.transform.position + offset;
	}
}
