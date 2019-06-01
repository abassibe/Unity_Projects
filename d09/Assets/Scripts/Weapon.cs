using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject originPoint;

    private float dist;

    private float counter;

    public float lineDrawSpeed = 6;

    public LineRenderer line;

    Vector3 destination;

    public Camera cam;

    public float rannge = 200;

    public GameObject particule;

    private GameObject copyParticule;

    ParticleSystem.EmissionModule em1;

    public float fireRate;

    private float timer;

    public Transform recoilMod;

    float maxRecoil_x = -20;

    float recoilSpeed = 10;

    float recoil = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && timer + fireRate < Time.time)
        {
            timer = Time.time;
            if (em1.enabled)
                em1.enabled = false;
            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            line.SetPosition(0, originPoint.transform.position);

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, rannge))
                line.SetPosition(1, hit.point);
            else
                line.SetPosition(1, rayOrigin + (cam.transform.forward * rannge));
            recoil += 0.1f;
        }
        if (name == "GUN")
            recoiling();
    }

    private IEnumerator ShotEffect()
    {
        GetComponent<AudioSource>().Play();

        line.enabled = true;

        yield return new WaitForSeconds(0.2f);

        if (copyParticule)
            Destroy(copyParticule);
        copyParticule = Instantiate(particule, line.GetPosition(1), Quaternion.identity);
        em1 = copyParticule.GetComponent<ParticleSystem>().emission;
        em1.enabled = true;
        particule.transform.position = line.GetPosition(1);
        if (name == "GUN")
            StartCoroutine(activeParticle());
        else
            StartCoroutine(activeAOE());
        line.enabled = false;
    }

    private IEnumerator activeParticle()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(copyParticule);
    }

    private IEnumerator activeAOE()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(copyParticule);
    }

    void recoiling()
    {
        if (recoil > 0)
        {
            Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x, 0, 0);
            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, maxRecoil, Time.deltaTime * recoilSpeed);
            Vector3 tmp = transform.localEulerAngles;
            tmp.x = recoilMod.localEulerAngles.x;
            transform.localEulerAngles = tmp;
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0;
            Quaternion minRecoil = Quaternion.Euler(0, 0, 0);
            recoilMod.rotation = Quaternion.Slerp(recoilMod.rotation, minRecoil, Time.deltaTime * recoilSpeed / 2);
            Vector3 tmp = transform.localEulerAngles;
            tmp.x = recoilMod.localEulerAngles.x;
            transform.localEulerAngles = tmp;
        }
    }
}

