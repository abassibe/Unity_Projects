using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;

    private Rigidbody2D body;
    Vector2 direction;

    string shooter;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        // GetComponent<Renderer>().enabled = false;
        // GetComponent<BoxCollider2D>().enabled = false;
    }

    public void go(Vector2 pos, Vector2 dir, string t)
    {
        gameObject.SetActive(true);
        shooter = t;
        // GetComponent<Renderer>().enabled = true;
        // GetComponent<BoxCollider2D>().enabled = true;
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ammo" || collider.tag == shooter || collider.tag == "weapon")
        {
            // GameObject.Destroy(this);
            return;
        }        //if (collider.GetComponent<Missile>() != null)
        //   return;
        var character = collider.GetComponent<Character>();
        if (!character)
            gameObject.SetActive(false);
        else
            character.isHitten();
        GameObject.Destroy(this);
    }

    void Update()
    {
        if (gameObject.activeSelf)
            body.velocity = direction * speed * 30;
    }
}