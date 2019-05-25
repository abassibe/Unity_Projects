using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public int HP = 1;
    //public     Gun             gun = null;
    public float speed = 4f;

    protected Renderer sprite;
    protected Rigidbody2D body;
    protected Vector2 direction = Vector2.zero;

    public void isHitten()
    {
        HP--;
        if (HP == 0)
            StartCoroutine(blink(4, 0.2));
    }
    IEnumerator blink(int repeat, double speed)
    {
        var now = Time.time;
        for (int i = repeat * 2; i > 0; i--)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds((float)speed);
        }
    }
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<Renderer>();
    }
    void move()
    {
        body.velocity = direction * speed;
    }
    void FixedUpdate()
    {
        move();
    }
}
