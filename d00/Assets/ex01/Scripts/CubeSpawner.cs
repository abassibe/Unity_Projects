using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

	public GameObject	aCube;
	public GameObject	sCube;
	public GameObject	dCube;
	private List<GameObject> obj;
	void Start()
	{
		obj = new List<GameObject>();
	}
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		switch (Random.Range(0, 200))
		{
			case 1:
			obj.Add(GameObject.Instantiate(aCube, aCube.transform.position, Quaternion.identity));
			break;
			case 2:
			obj.Add(GameObject.Instantiate(sCube, sCube.transform.position, Quaternion.identity));
			break;
			case 3:
			obj.Add(GameObject.Instantiate(dCube, dCube.transform.position, Quaternion.identity));
			break;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			for (int i = 0; i < obj.Count; i++)
			{
				if (obj[i].name.StartsWith("A") && obj[i].transform.localPosition.y < -2 && obj[i].transform.localPosition.y >= -3)
				{
					float accuracy = 100 - ((3 - (obj[i].transform.localPosition.y * -1)) * 100);
					Debug.Log("Precision: " + accuracy);
					GameObject.Destroy(obj[i]);
					obj.RemoveAt(i);
					i--;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			for (int i = 0; i < obj.Count; i++)
			{
				if (obj[i].name.StartsWith("S") && obj[i].transform.localPosition.y < -2 && obj[i].transform.localPosition.y >= -3)
				{
					float accuracy = 100 - ((3 - (obj[i].transform.localPosition.y * -1)) * 100);
					Debug.Log("Precision: " + accuracy);
					GameObject.Destroy(obj[i]);
					obj.RemoveAt(i);
					i--;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			for (int i = 0; i < obj.Count; i++)
			{
				if (obj[i].name.StartsWith("D") && obj[i].transform.localPosition.y < -2 && obj[i].transform.localPosition.y >= -3)
				{
					float accuracy = 100 - ((3 - (obj[i].transform.localPosition.y * -1)) * 100);
					Debug.Log("Precision: " + accuracy);
					GameObject.Destroy(obj[i]);
					obj.RemoveAt(i);
					i--;
				}
			}
		}

		for (int i = 0; i < obj.Count; i++)
		{
			if (obj[i].transform.localPosition.y < -3)
			{
				GameObject.Destroy(obj[i]);
				obj.RemoveAt(i);
				i--;
			}
		}
	}
}
