using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip sound;
    public Missile missile_;
    public Sprite onBody;
    public int ammo;
    public double rateSpeed;
    public float range = 10f;
    [HideInInspector]
    public Sprite groundSprite;
    [HideInInspector]
    public GameObject owner = null;
    public List<GameObject> missiles = new List<GameObject>();
    private double timer = 0;
    protected bool shot = false;
    [HideInInspector]
    public int index;

    void Start()
    {
        groundSprite = GetComponent<SpriteRenderer>().sprite;
        index = ammo - 1;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!owner && Input.GetKey("e") && collider.gameObject.tag == "Player")
        {
            owner = collider.gameObject;
            owner.GetComponent<You>().getGun(this);
        }
    }

    public IEnumerator roll()
    {
        double time = 0;
        while (time < 1.5)
        {
            time += Time.deltaTime;
            transform.Rotate(0, 0, 10, Space.Self);
            yield return null;
        }
        this.GetComponent<BoxCollider2D>().isTrigger = true;

    }
    public void shoot_real(Vector2 direction)
    {
        Missile missile = Instantiate(missile_);
        missile.transform.position = transform.position;
        missile.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 90);
        missile.go(transform.position, direction, owner.tag);
        index--;
        shot = true;
        timer = 0;
        AudioManager.audio.playSound(sound);
    }

    public void shoot(Vector2 direction)
    {
        if (shot && index > 0)
        {
            if (timer > rateSpeed)
            {
                shot = false;
                timer = 0;
            }
        }
        else if (index > 0)
            shoot_real(direction);
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}