using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;

    private float timer;

    private float limit = 0;

    public int count = 4;

    void Update()
    {
        if (count < 20 && timer + limit < Time.time)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    spawner1.GetComponent<Spawner>().invokeZonzon();
                    count += 1;
                    break;
                case 1:
                    spawner2.GetComponent<Spawner>().invokeZonzon();
                    count += 1;
                    break;
                case 2:
                    spawner3.GetComponent<Spawner>().invokeZonzon();
                    count += 1;
                    break;
                case 3:
                    spawner4.GetComponent<Spawner>().invokeZonzon();
                    count += 1;
                    break;
            }
            limit = Random.Range(1, 10);
            timer = Time.time;
        }
    }
}
