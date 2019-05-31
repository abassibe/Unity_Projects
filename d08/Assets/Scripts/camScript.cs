using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    public GameObject target;

    public Vector3 offset;

    void Start()
    {
    }

    void Update()
    {
        transform.LookAt(target.transform);
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;
    }
}
