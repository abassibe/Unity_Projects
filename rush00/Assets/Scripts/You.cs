using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You: Character {
	
	float	getDirectionAngle(){
		var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		return Mathf.Atan2(
				direction.normalized.y,
				direction.normalized.x
				) * (180 / Mathf.PI);
	}
	void Update(){
		transform.rotation = Quaternion.Euler(0, 0, getDirectionAngle() + 90);
		direction.x = Input.GetAxis("Horizontal");
		direction.y = Input.GetAxis("Vertical");
	}
}

