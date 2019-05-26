using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class You : Character
{
    protected override void die()
    {
        Destroy(gameObject);
        GameManager.gm.gameOver();
    }
    public void getGun(Gun g)
    {
        if (gun)
            return;
        gun = g;
        base.getGun();
    }
    public void throwGun()
    {
        if (gun)
        {
            gun.GetComponent<SpriteRenderer>().sprite = gun.groundSprite;
            gun.transform.parent = null;
            StartCoroutine(gun.roll());
            var body = gun.GetComponent<Rigidbody2D>();
            body.isKinematic = false;
            gun.GetComponent<BoxCollider2D>().isTrigger = false;
            body.AddForce(toMouse() * 4f, ForceMode2D.Impulse);
            gun = null;
        }
    }
    public Vector2 toMouse()
    {
        var distance =
            Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - transform.position;
        return new Vector2(distance.x, distance.y).normalized;
    }
    public float getDirectionAngle()
    {
        var direction = toMouse();
        return Mathf.Atan2(
                direction.y,
                direction.x
                ) * (180 / Mathf.PI);
    }
    void setHUD()
    {
        var hud = "Gros Gun : ";
        if (gun)
        {
            hud += gun.name;
            hud += "\nGrosses ammos: " + (gun.index + 1);
        }
        else
            hud += "que dalle";
        // GameManager.gm.setHUD(hud);
    }
    void Update()
    {
        setHUD();
        transform.rotation = Quaternion.Euler(0, 0, getDirectionAngle() + 90);
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if (gun)
        {
            if (Input.GetMouseButton(0))
                gun.shoot(toMouse());
            else if (Input.GetMouseButton(1))
            {
                gun.owner = null;
                throwGun();
            }
        }
    }
}

