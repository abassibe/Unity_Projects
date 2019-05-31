using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] prefab;

    public GameObject zonzon;

    private float timer;

    private float limit = 0;

    void Start()
    {
        switch (Random.Range(0, 1))
        {
            case 0:
                zonzon = GameObject.Instantiate(prefab[0], transform.position, Quaternion.identity);
                break;
            case 1:
                zonzon = GameObject.Instantiate(prefab[1], transform.position, Quaternion.identity);
                break;
        }
        timer = Time.time;
    }

    void Update()
    {
        if (timer + limit < Time.time && !zonzon)
        {
            switch (Random.Range(0, 1))
            {
                case 0:
                    zonzon = GameObject.Instantiate(prefab[0], transform.position, Quaternion.identity);
                    break;
                case 1:
                    zonzon = GameObject.Instantiate(prefab[1], transform.position, Quaternion.identity);
                    break;
            }
            limit = Random.Range(3, 10);
            timer = Time.time;
        }
    }
}
