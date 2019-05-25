using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject missile_;
    public int rate;
    public double rateSpeed;
    public double reload;

    private GameObject owner = null;
    public List<GameObject> missiles = new List<GameObject>();
    private double timer = 0;
    protected bool shot = false;
    private int index;

    void Start()
    {
        index = rate - 1;
        for (int i = rate; i > 0; --i)
        {
            var m = Instantiate(missile_);
            missiles.Add(m);
        }
    }

    public void shoot()
    {
        var direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        missiles[index].GetComponent<Missile>().go(transform.position, direction, transform.rotation);
        index--;
        shot = true;
        timer = 0;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!owner && Input.GetKey("e") && collider.gameObject.tag == "Hero")
        {
            owner = collider.gameObject;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    void Update()
    {
        if (owner)
        {
            transform.position = owner.transform.position;
            //transform.position.x += 0.3f;
            transform.rotation = owner.transform.rotation;
            timer += Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                if (shot)
                {
                    if (timer > rateSpeed)
                    {
                        shot = false;
                        timer = 0;
                    }
                }
                else if (index == -1)
                {
                    if (timer > reload)
                    {
                        timer = 0;
                        index = rate - 1;
                    }
                }
                else
                    shoot();
            }
        }
    }
}
