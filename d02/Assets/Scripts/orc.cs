using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orc : MonoBehaviour
{
	public int lifePoint;

	public AudioClip isKilledSound;

	public AudioSource audioSource;

	private bool isDiying = false;

	public Vector3 mainTarget;

	void Start ()
	{
	}
	
	void Update ()
	{
		if (lifePoint == 0 && !isDiying)
		{
			audioSource.PlayOneShot(isKilledSound);
			isDiying = true;
		}
		if (!audioSource.isPlaying && isDiying)
			GameObject.Destroy(this.gameObject);
	}
}
