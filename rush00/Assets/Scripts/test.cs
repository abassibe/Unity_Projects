using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Transform rotator;
    float speed = 1.0f;
    float halfCubeSize = 0.5f;
    private bool rotating = false;

    void RotateCube(Vector2 refPoint, Vector2 rotationAxis)
    {
        rotator.localRotation = Quaternion.identity;
        rotator.position = (Vector2)transform.position - Vector2.left * halfCubeSize + refPoint;
        transform.parent = rotator;
        float angle = 0f;
        while (angle < 90.0)
        {
            angle += Time.deltaTime * 90.0f * speed;
            rotator.rotation = Quaternion.AngleAxis(Mathf.Min(angle, 90.0f), rotationAxis);
            // yield;
        }
        transform.parent = null;
        rotating = false;
    }


    void Start()
    {
        rotator = (new GameObject("Rotator")).transform;
    }

    void Update()
    {
        // transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // transform.localPosition = new Vector2(-1.5f, -1f);
        // if (!rotating)
        // {
        //     if (Input.GetKey(KeyCode.D))
        //     {
        //         rotating = true;
        //         RotateCube(Vector3.right * halfCubeSize, -Vector3.forward);
        //     }
        //     else if (Input.GetKey(KeyCode.A))
        //     {
        //         rotating = true;
        //         RotateCube(-Vector3.right * halfCubeSize, Vector3.forward);
        //     }
        //     else if (Input.GetKey(KeyCode.W))
        //     {
        //         rotating = true;
        //         RotateCube(Vector3.forward * halfCubeSize, Vector3.right);
        //     }
        //     else if (Input.GetKey(KeyCode.S))
        //     {
        //         rotating = true;
        //         RotateCube(-Vector3.forward * halfCubeSize, -Vector3.right);
        //     }
        // }
    }
}
