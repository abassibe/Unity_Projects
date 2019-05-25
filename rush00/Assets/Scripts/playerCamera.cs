using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public GameObject actualPlayer;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - actualPlayer.transform.position;
    }

    void LateUpdate()
    {
        transform.position = actualPlayer.transform.position + offset;
    }
}