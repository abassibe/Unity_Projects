using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canon : MonoBehaviour
{
    public float speed = 1.0f;

    public GameObject gunShot;

    public GameObject gunImpact; // TODO: Rajouter au point d'impact

    public GameObject missileShot;

    private int missile = 8;

    public AudioSource gunMusic;

    public AudioSource missileMusic;

    public AudioSource missMusic;

    public AudioClip tst;

    private float limitSound;

    public GameObject body;

    public Text lifeLeft;

    public Text ammoLeft;

    void Update()
    {
        lifeLeft.text = "Life point\n" + body.GetComponent<body>().HP.ToString();
        ammoLeft.text = "Missiles left\n" + missile.ToString();
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
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
            GameObject cpy = Instantiate(gunShot, hit.point, Quaternion.identity);
            ParticleSystem.EmissionModule em = cpy.GetComponent<ParticleSystem>().emission;
            em.enabled = true;
            Destroy(cpy, 1);
        }
        if (Input.GetMouseButtonDown(1) && missile > 0)
        {
            missileMusic.Play();
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            Physics.Raycast(transform.position, fwd, out hit, 60);
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
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Gun Shoot(Clone)")
            body.GetComponent<body>().HP -= 5;
        if (col.gameObject.name == "MissileShoot(Clone)")
            body.GetComponent<body>().HP -= 30;
    }
}
