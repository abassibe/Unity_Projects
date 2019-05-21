using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlateform : MonoBehaviour {

	public Vector2 originPosition;
	public Vector2 targetPosition;

	public float speed;

	private bool directionIsUp;

	private bool directionIsLeft;

	void Update ()
	{
		Vector3 newValue = new Vector3(0, 0, 0);
		if (originPosition.y != targetPosition.y)
		{
			if (directionIsUp)
			{
				newValue.y = speed;
				if (gameObject.transform.localPosition.y >= originPosition.y)
					directionIsUp = false;
			}
			else
			{
				newValue.y = -speed;
				if (gameObject.transform.localPosition.y <= targetPosition.y)
					directionIsUp = true;
			}
		}
		if (originPosition.x != targetPosition.x)
		{
			if (directionIsLeft)
			{
				newValue.x = -speed;
				if (gameObject.transform.localPosition.x <= targetPosition.x)
					directionIsLeft = false;
			}
			else
			{
				newValue.x = speed;
				if (gameObject.transform.localPosition.x >= originPosition.x)
					directionIsLeft = true;
			}
		}
		gameObject.transform.Translate(newValue);
	}
}
