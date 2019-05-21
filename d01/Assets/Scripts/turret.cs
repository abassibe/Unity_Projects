using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
	public GameObject yellowShot;

	public GameObject redShot;

	float lastRedShot = 0;

	float lastYellowShot = 0;

	public float shotRate;

	void Update ()
	{
		if (Math.Abs(Time.time - lastRedShot) > shotRate && transform.rotation.z > 0)
		{
			Vector3 pos = transform.position;
			pos.x = 4.3f;
			GameObject.Instantiate(redShot, pos, Quaternion.identity);
			lastRedShot = Time.time;
		}
		if (Math.Abs(Time.time - lastYellowShot) > shotRate && transform.rotation.z < 0)
		{
			Vector3 pos = transform.position;
			pos.x = 11.4f;
			GameObject.Instantiate(yellowShot, pos, Quaternion.identity);
			lastYellowShot = Time.time;
		}
	}
}
