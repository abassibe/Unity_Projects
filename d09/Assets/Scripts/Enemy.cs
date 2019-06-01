using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public Animator anim;

    private Vector3 dest = new Vector3(60.87f, 30.12f, 54.47f);

    public int HP = 2;

    private bool routineIsRunning;

    public waveManager wm;

    void Update()
    {
        if (HP <= 0)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            StartCoroutine(die());
            return;
        }
        if (anim.GetBool("takeDamage"))
            return;
        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            transform.LookAt(player.transform.position);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
            if (!routineIsRunning)
                StartCoroutine(attack());
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            dest = player.transform.position;
            GetComponent<NavMeshAgent>().isStopped = false;
            transform.LookAt(dest);
            GetComponent<NavMeshAgent>().destination = dest;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
        else if (Vector3.Distance(transform.position, dest) > 3)
        {
            transform.LookAt(dest);
            GetComponent<NavMeshAgent>().destination = dest;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Fire")
        {
            dest = col.transform.position;
            HP -= 1;
            StartCoroutine(takeSHot());
        }
    }

    private IEnumerator takeSHot()
    {
        bool run = anim.GetBool("isRunning");
        bool walk = anim.GetBool("isWalking");
        bool attack = anim.GetBool("isAttacking");
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("takeDamage", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("takeDamage", false);
        anim.SetBool("isRunning", run);
        anim.SetBool("isWalking", walk);
        anim.SetBool("isAttacking", attack);
    }

    private IEnumerator die()
    {
        anim.SetBool("isDiying", true);
        yield return new WaitForSeconds(3);
        wm.count -= 1;
        Destroy(gameObject);
    }

    private IEnumerator attack()
    {
        routineIsRunning = true;
        yield return new WaitForSeconds(1.3f);
        routineIsRunning = false;
        player.GetComponent<Player>().HP -= 10;
    }
}
