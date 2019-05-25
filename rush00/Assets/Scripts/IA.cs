using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    bool isTriggered = false;
    CircleCollider2D pov;
    PolygonCollider2D areaPov;

    public Character character;

    public Gun gun;

    void Start()
    {
        pov = GetComponent<CircleCollider2D>();
        areaPov = GetComponent<PolygonCollider2D>();
        gun = GameObject.Instantiate(gun, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (isTriggered)
        {
            Vector2 direction = (character.gameObject.transform.position - transform.position).normalized;
            Vector2 origin = (Vector2)transform.position + direction;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, /*gun.range*/1000);

            Vector2 targetDir = hit.point - (Vector2)transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            if (characterIsOnView())
            {
                gun.shoot();
            }
        }
        else
        {
            findPOV();
        }
    }

    public void goTo(Character character)
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Vector2 direction = (character.gameObject.transform.position - transform.position).normalized;
            Vector2 origin = (Vector2)transform.position + direction;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, /*gun.range*/1000);
            if (hit.collider.name == "Character")
                isTriggered = true;
        }
    }

    bool characterIsOnView()
    {
        Vector2 direction = (character.gameObject.transform.position - transform.position).normalized;
        Vector2 origin = (Vector2)transform.position + direction;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, /*gun.range*/1000);
        if (hit.collider.name == "Character")
            return true;
        return false;
    }

    void shoot()
    {

    }

    void findPOV()
    {

    }
}