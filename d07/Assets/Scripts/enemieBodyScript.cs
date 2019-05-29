using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemieBodyScript : MonoBehaviour
{
    public float speed;

    public float rotate;

    public Vector3 wayPoint;

    void Awake()
    {
        wayPoint = new Vector3(25, 30, 88);
        GetComponent<NavMeshAgent>().destination = wayPoint;
    }

    void FixedUpdate()
    {
        GetComponent<NavMeshAgent>().destination = wayPoint;
        if (speed > 1000)
        {
            transform.Translate(new Vector3(0, 0, speed));
            transform.Translate(new Vector3(0, 0, -speed));
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(0, -rotate, 0);
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(0, rotate, 0);
        }
    }
}
