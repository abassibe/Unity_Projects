using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	public float speed;
	void Start () {
		speed = Random.Range(0.05f, 0.15f);
	}
	
	void Update () {
		transform.Translate(new Vector3(0, speed * -1, 0));
	}
}
