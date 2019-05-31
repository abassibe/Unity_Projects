using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerScript : MonoBehaviour
{
    private Vector3 pointToReach;

    public int HP;

    public int strengh = 15;

    public int agility = 15;

    public int constitution = 15;

    public int armor = 15;

    public int minDamage;

    public int maxDamage;

    public int lvl = 1;

    public int xp = 0;

    public int xpNextLevel = 100;

    public int money = 0;

    public AnimationClip death;

    private GameObject target = null;

    private bool routineIsRunning = false;

    void Start()
    {
        pointToReach = transform.position;
        HP = 5 * constitution;
        minDamage = strengh / 2;
        maxDamage = minDamage + 4;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0))
        {
            target = null;
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
        else if (Input.GetMouseButton(0))
        {
            if (!target && Physics.Raycast(ray, out hit, 100) && Vector3.Distance(hit.point, GetComponent<NavMeshAgent>().transform.position) > GetComponent<NavMeshAgent>().stoppingDistance)
            {
                pointToReach = hit.point;
                transform.LookAt(hit.point);
                GetComponent<Animator>().SetBool("isWalking", true);
                GetComponent<Animator>().SetBool("isAttacking", false);
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
            else if (Physics.Raycast(ray, out hit, 100) && Vector3.Distance(hit.point, GetComponent<NavMeshAgent>().transform.position) < GetComponent<NavMeshAgent>().stoppingDistance && hit.collider.tag == "Enemy")
            {
                target = hit.collider.gameObject;
            }
            if (target && !routineIsRunning)
            {
                transform.LookAt(hit.point);
                GetComponent<Animator>().SetBool("isWalking", false);
                StartCoroutine(attackOnceAnim());
            }
        }
        if (Vector3.Distance(pointToReach, GetComponent<NavMeshAgent>().transform.position) < GetComponent<NavMeshAgent>().stoppingDistance && GetComponent<Animator>().GetBool("isWalking"))
            GetComponent<Animator>().SetBool("isWalking", false);
        if (HP <= 0)
        {
            GetComponent<Animator>().SetInteger("isDead", GetComponent<Animator>().GetInteger("isDead") + 1);
        }
    }

    public IEnumerator attackOnceAnim()
    {
        GameObject cpy = target;
        routineIsRunning = true;
        GetComponent<Animator>().SetBool("isAttacking", true);
        if (Random.Range(0, 101) <= 75 + agility - cpy.GetComponent<Enemies>().agility)
            cpy.GetComponent<Enemies>().getDamage(Random.Range(minDamage, maxDamage) * (1 - cpy.GetComponent<Enemies>().armor / 200));
        yield return new WaitForSeconds(0.767f);
        routineIsRunning = false;
    }

    public void getDamage(int amount)
    {
        HP -= amount;
    }
}
