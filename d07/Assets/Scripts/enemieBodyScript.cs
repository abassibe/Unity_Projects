using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemieBodyScript : MonoBehaviour
{
    public Vector3 wayPoint = Vector3.zero;

    public GameObject canon;

    void Start()
    {
        getClosestEnemy();
        GetComponent<NavMeshAgent>().destination = wayPoint;
    }

    void FixedUpdate()
    {
        if (canon.GetComponent<enemieCanonScript>().HP <= 0)
        {
            canon.GetComponent<enemieCanonScript>().crossHair.color = Color.black;
            GameObject.Destroy(gameObject);
            return;
        }
        if (canon.GetComponent<enemieCanonScript>().isMissileRange)
            GetComponent<NavMeshAgent>().isStopped = true;
        else
        {
            getClosestEnemy();
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().destination = wayPoint;
        }
    }

    void getClosestEnemy()
    {
        float range = 50;
        while (range < 500)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag != "Terrain" && hitColliders[i].tag != "Ammo" && hitColliders[i].tag != gameObject.tag)
                {
                    wayPoint = hitColliders[i].transform.position;
                    wayPoint.y += 3;
                    canon.GetComponent<enemieCanonScript>().targetPlayer = hitColliders[i].transform.position;
                    return;
                }
                i++;
            }
            range += 10;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Gun Shoot(Clone)")
        {
            canon.GetComponent<enemieCanonScript>().HP -= 5;
            if (!canon.GetComponent<enemieCanonScript>().routineStart)
                StartCoroutine(canon.GetComponent<enemieCanonScript>().changeColor());
        }
        if (col.gameObject.name == "MissileShoot(Clone)")
        {
            if (!canon.GetComponent<enemieCanonScript>().routineStart)
                StartCoroutine(canon.GetComponent<enemieCanonScript>().changeColor());
            canon.GetComponent<enemieCanonScript>().HP -= 30;
        }
    }
}
