using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemieCanonScript : MonoBehaviour
{
    public GameObject gunShot;

    public GameObject gunImpact;

    public GameObject missileShot;

    private int missile = 8;

    public AudioSource gunMusic;

    public AudioSource missileMusic;

    public AudioSource missMusic;

    public AudioClip tst;

    private float limitSound;

    public Vector3 targetPlayer;

    public bool isRifleRange = false;

    public bool isMissileRange = false;

    public float lastSalve = 0.0f;
    public float lastMissile = 0.0f;

    private IEnumerator routineRifle = null;

    private IEnumerator routineMissile = null;

    public float accuracy;

    public int HP = 100;

    public Image crossHair;

    public bool routineStart = false;

    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        Vector3 target = transform.TransformDirection(Vector3.forward);
        Vector3 toLook = targetPlayer;
        toLook.y += 1;
        transform.LookAt(toLook);
        RaycastHit hits;
        if (Physics.Raycast(transform.position, target, out hits, 80) && hits.collider.tag != gameObject.tag)
            isRifleRange = true;
        else
            isRifleRange = false;
        if (Physics.Raycast(transform.position, target, out hits, 60) && hits.collider.tag != gameObject.tag)
            isMissileRange = true;
        else
            isMissileRange = false;
        if (hits.collider && hits.collider.tag == "Ammo")
        {
            isRifleRange = true;
            isMissileRange = true;
        }
        if (!isRifleRange && !isMissileRange)
            return;

        if (lastSalve + 3 < Time.time && isRifleRange)
        {
            if (routineRifle == null)
            {
                routineRifle = shotRifle();
                StartCoroutine(routineRifle);
            }
        }
        if (lastMissile + 5 < Time.time && isMissileRange && missile > 0)
        {
            if (routineMissile == null)
            {
                routineMissile = shotMissile();
                StartCoroutine(routineMissile);
            }
        }
    }

    public IEnumerator shotRifle()
    {
        for (int i = 0; i < 5; i++)
        {
            lastSalve = Time.time;
            if (limitSound + 0.2 < Time.time)
            {
                gunMusic.PlayOneShot(tst);
                limitSound = Time.time;
            }
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            Physics.Raycast(transform.position, fwd, out hit, 80);
            GameObject cpy1 = Instantiate(gunShot, gunShot.transform.position, Quaternion.identity);
            ParticleSystem.EmissionModule em1 = cpy1.GetComponent<ParticleSystem>().emission;
            em1.enabled = true;
            Destroy(cpy1, 1);
            GameObject cpy = Instantiate(gunShot, hit.point + (Random.insideUnitSphere * accuracy), Quaternion.identity);
            ParticleSystem.EmissionModule em = cpy.GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            Destroy(cpy, 1);
            yield return new WaitForSeconds(0.2f);
        }
        routineRifle = null;

    }

    public IEnumerator shotMissile()
    {
        lastMissile = Time.time;
        if (limitSound + 0.2 < Time.time)
        {
            gunMusic.PlayOneShot(tst);
            limitSound = Time.time;
        }
        missileMusic.Play();
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Physics.Raycast(transform.position, fwd, out hit, 60);
        GameObject cpy1 = Instantiate(gunShot, gunShot.transform.position, Quaternion.identity);
        ParticleSystem.EmissionModule em1 = cpy1.GetComponent<ParticleSystem>().emission;
        em1.enabled = true;
        Destroy(cpy1, 1);
        GameObject cpy = Instantiate(missileShot, hit.point + (Random.insideUnitSphere * accuracy), Quaternion.identity);
        ParticleSystem.EmissionModule em = cpy.GetComponent<ParticleSystem>().emission;
        em.enabled = true;
        Destroy(cpy, 1);
        missile--;
        routineMissile = null;
        yield return null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Gun Shoot(Clone)")
        {
            HP -= 5;
            if (!routineStart)
                StartCoroutine(changeColor());
        }
        if (col.gameObject.name == "MissileShoot(Clone)")
        {
            HP -= 30;
            if (!routineStart)
                StartCoroutine(changeColor());
        }
    }

    public IEnumerator changeColor()
    {
        routineStart = true;
        crossHair.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        crossHair.color = Color.black;
        routineStart = false;
    }
}
