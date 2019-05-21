using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switcher : MonoBehaviour
{
	static bool blueIsOpen = false;
	Vector3 blueOrigin = new Vector3(-3.871f, 1.2126f, 0);
	Vector3 blueRotate = new Vector3(-3.505f, 1.651f, 0);
	static bool greenIsOpen = false;
	Vector3 greenOrigin = new Vector3(-3.863f, 0.3725f, 0);
	Vector3 greenRotate = new Vector3(-3.538f, 0.7731f, 0);
	static bool redIsOpen = false;
	Vector3 redOrigin = new Vector3(-0.044f, 0.3717f, 0);
	Vector3 redRotate = new Vector3(0.273f, 0.766f, 0);
	static 	bool yellowIsOpen = false;
	Vector3 yellowOrigin = new Vector3(-1.2531f, 2.0752f, 0);
	Vector3 yellowRotate = new Vector3(-0.898f, 2.495f, 0);

	bool placeOccuped = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (placeOccuped)
			return ;
		placeOccuped = true;
		GameObject door;
		switch (gameObject.name)
		{
			case "greenSwitcher":
				if (greenIsOpen)
					break;
				door = GameObject.Find("greenDoor");
				door.transform.localPosition = greenRotate;
				door.transform.Rotate(0, 0, 90);
				greenIsOpen = true;
				break;
			case "blueSwitcher":
				if (blueIsOpen)
					break;
				door = GameObject.Find("blueDoor");
				door.transform.localPosition = blueRotate;
				door.transform.Rotate(0, 0, 90);
				blueIsOpen = true;
				break;
			case "whiteSwitcher":
				switch (collision.gameObject.name)
				{
					case "red":
						if (redIsOpen)
							break;
						door = GameObject.Find("redDoor");
						door.transform.localPosition = redRotate;
						door.transform.Rotate(0, 0, 90);
						redIsOpen = true;
						break;
					case "yellow":
						if (yellowIsOpen)
							break;
						door = GameObject.Find("yellowDoor");
						door.transform.localPosition = yellowRotate;
						door.transform.Rotate(0, 0, 90);
						yellowIsOpen = true;
						break;
					case "blue":
						if (blueIsOpen)
							break;
						door = GameObject.Find("blueDoor");
						door.transform.localPosition = blueRotate;
						door.transform.Rotate(0, 0, 90);
						blueIsOpen = true;
						break;
					default:
						break;
				}
				break;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		placeOccuped = false;
		GameObject door;
		switch (gameObject.name)
		{
			case "greenSwitcher":
				if (!greenIsOpen)
					break;
				door = GameObject.Find("greenDoor");
				door.transform.localPosition = greenOrigin;
				door.transform.Rotate(0, 0, -90);
				greenIsOpen = false;
				break;
			case "blueSwitcher":
				if (!blueIsOpen)
					break;
				door = GameObject.Find("blueDoor");
				door.transform.localPosition = blueOrigin;
				door.transform.Rotate(0, 0, -90);
				blueIsOpen = false;
				break;
			case "whiteSwitcher":
				switch (collision.gameObject.name)
				{
					case "red":
						if (!redIsOpen)
							break;
						door = GameObject.Find("redDoor");
						door.transform.localPosition = redOrigin;
						door.transform.Rotate(0, 0, -90);
						redIsOpen = false;
						break;
					case "yellow":
						if (!yellowIsOpen)
							break;
						door = GameObject.Find("yellowDoor");
						door.transform.localPosition = yellowOrigin;
						door.transform.Rotate(0, 0, -90);
						yellowIsOpen = false;
						break;
					case "blue":
						if (!blueIsOpen)
							break;
						door = GameObject.Find("blueDoor");
						door.transform.localPosition = blueOrigin;
						door.transform.Rotate(0, 0, -90);
						blueIsOpen = false;
						break;
					default:
						break;
				}
				break;
		}
	}
}
