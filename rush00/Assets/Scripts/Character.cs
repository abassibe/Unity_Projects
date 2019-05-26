using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int HP = 1;
    public Gun gun = null;
    public float speed = 4f;

    protected Rigidbody2D body;
    protected Vector2 direction = Vector2.zero;
    protected Animator animator;
    public List<Sprite> heads = new List<Sprite>();
    public List<Sprite> bodies = new List<Sprite>();

    public void isHitten()
    {
        HP--;
        if (HP == 0)
            StartCoroutine(dieBlinking(4, 0.2));
    }
    protected T pick<T>(List<T> list)
    {
        var index = Random.Range(0, list.Count - 1);
        return list[index];
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        body = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = pick<Sprite>(heads);
        transform.Find("body").GetComponent<SpriteRenderer>().sprite = pick<Sprite>(bodies);
    }

    public void getGun()
    {
        gun.GetComponent<SpriteRenderer>().sprite = gun.onBody;
        gun.GetComponent<Rigidbody2D>().isKinematic = true;
        gun.transform.position =
            (Vector2)transform.position
               - (Vector2)transform.up * 0.2f;
        gun.transform.rotation = transform.rotation;
        gun.GetComponent<SpriteRenderer>().sortingOrder = 1;
        gun.transform.parent = transform;
    }

    IEnumerator dieBlinking(int repeat, double speed)
    {
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        for (int i = repeat * 2; i > 0; i--)
        {
            var children = GetComponentsInChildren<SpriteRenderer>();
            foreach (var child in children)
                child.enabled = !child.enabled;
            yield return new WaitForSeconds((float)speed);
        }
        die();
    }
    virtual protected void die()
    {
        Destroy(gameObject);
        GameManager.gm.onePoint();
    }
    void move()
    {
        var walking = animator.GetBool("walk");
        if (walking && direction == Vector2.zero)
            animator.SetBool("walk", false);
        else if (!walking && direction != Vector2.zero)
            animator.SetBool("walk", true);
        body.velocity = direction * speed;
    }
    void FixedUpdate()
    {
        move();
    }
}
