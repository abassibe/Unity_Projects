using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float speed;

    Vector2 direction;

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }

    public void go(Vector2 pos, Vector2 dir, Quaternion rotation)
    {
        GetComponent<Renderer>().enabled = true;
        transform.position = pos;
        //transform.rotation = rotation;
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<Character>().isHitten();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.Translate(direction * speed);
        }
    }
}
