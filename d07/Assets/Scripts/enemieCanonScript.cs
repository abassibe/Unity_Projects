using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieCanonScript : MonoBehaviour
{
    public float speed = 1.0f;

    public GameObject gunShot;

    public GameObject gunImpact;

    public GameObject missileShot;

    private int missile = 8;

    public AudioSource gunMusic;

    public AudioSource missileMusic;

    public AudioSource missMusic;

    public AudioClip tst;

    private float limitSound;

    private bool isShooting = false;

    void Update()
    {
        // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
        // GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (isShooting)
        {
            if (limitSound + 0.2 < Time.time)
            {
                gunMusic.PlayOneShot(tst);
                limitSound = Time.time;
            }
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fwd, out hit, 120))
                Debug.DrawLine(transform.position, hit.point, Color.red);
            GameObject cpy1 = Instantiate(gunShot, gunShot.transform.position, Quaternion.identity);
            ParticleSystem.EmissionModule em1 = cpy1.GetComponent<ParticleSystem>().emission;
            em1.enabled = true;
            Destroy(cpy1, 1);
            GameObject cpy = Instantiate(gunShot, hit.point, Quaternion.identity);
            ParticleSystem.EmissionModule em = cpy.GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            Destroy(cpy, 1);
        }
        if (isShooting && missile > 0)
        {
            missileMusic.Play();
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fwd, out hit, 60))
                Debug.DrawLine(transform.position, hit.point, Color.red);
            GameObject cpy1 = Instantiate(gunShot, gunShot.transform.position, Quaternion.identity);
            ParticleSystem.EmissionModule em1 = cpy1.GetComponent<ParticleSystem>().emission;
            em1.enabled = true;
            Destroy(cpy1, 1);
            GameObject cpy = Instantiate(missileShot, hit.point, Quaternion.identity);
            ParticleSystem.EmissionModule em = cpy.GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            Destroy(cpy, 1);
            missile--;
        }
    }
}
