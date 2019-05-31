using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public GameObject player;

    public int HP;

    public int strengh = 10;

    public int agility = 10;

    public int constitution = 10;

    public int armor = 10;

    public int minDamage;

    public int maxDamage;

    public int lvl = 1;

    public int xp = 0;

    public int money = 0;

    private bool routineIsDone = false;

    private IEnumerator routine;

    private float timer = 0;

    void Start()
    {
        HP = 5 * constitution;
        minDamage = strengh / 2;
        maxDamage = minDamage + 4;
    }

    void Update()
    {
        if (HP <= 0)
        {
            if (routineIsDone == true && routine != null)
                transform.Translate(0, -0.01f, 0);
            return;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
        {
            transform.LookAt(player.transform.position);
            GetComponent<Animator>().SetBool("isRunning", true);
            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<NavMeshAgent>().destination = player.transform.position;
            if (Vector3.Distance(transform.position, player.transform.position) < 3 && timer + 0.8 < Time.time)
            {
                GetComponent<Animator>().SetBool("isRunning", false);
                GetComponent<Animator>().SetBool("isAttacking", true);
                timer = Time.time;
                if (Random.Range(0, 101) <= 75 + agility - player.GetComponent<playerScript>().agility)
                {
                    player.GetComponent<playerScript>().getDamage(Random.Range(minDamage, maxDamage) * (1 - player.GetComponent<playerScript>().armor / 200));
                }
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
            GetComponent<Animator>().SetBool("isAttacking", false);
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    public void getDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            GetComponent<CharacterController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().Play("Death");
            routine = die();
            StartCoroutine(routine);
        }
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(4f);
        GameObject.Destroy(gameObject, 5);
        routineIsDone = true;
    }
}
