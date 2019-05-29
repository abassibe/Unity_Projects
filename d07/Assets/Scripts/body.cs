using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour
{
    public float speed;

    public float rotate;

    public float boost;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) && boost > 0)
            {
                transform.Translate(new Vector3(0, 0, speed * 2));
                boost -= 1f;
            }
            else
                transform.Translate(new Vector3(0, 0, speed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift) && boost > 0)
            {
                transform.Translate(new Vector3(0, 0, -(speed * 2)));
                boost -= 1f;
            }
            else
                transform.Translate(new Vector3(0, 0, -speed));
        }
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -rotate, 0);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, rotate, 0);
        if (boost < 100)
            boost += 0.3f;
    }
}
