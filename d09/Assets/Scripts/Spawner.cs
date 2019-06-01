using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zonzon;

    public GameObject zonzon2;

    void Start()
    {
        zonzon2 = GameObject.Instantiate(zonzon, transform.position, Quaternion.identity);
        zonzon2.SetActive(true);
    }

    public void invokeZonzon()
    {
        zonzon2 = GameObject.Instantiate(zonzon, transform.position, Quaternion.identity);
        zonzon2.SetActive(true);
    }
}
