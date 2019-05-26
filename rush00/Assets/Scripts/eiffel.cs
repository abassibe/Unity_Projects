using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class eiffel: MonoBehaviour {

	public GameObject	image;
	void Update () {
		var time = Time.time;
 		var transform = image.GetComponent<RectTransform>();
		//transform.Rotate(0, 0, 3);
		transform.localScale = new Vector3(
			time % 1.5f, 
			time % 1.5f, 1);
	}
}
