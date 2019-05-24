using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public Animator animator;

	public float speed;

	public AudioClip selectedSound;

	public AudioClip attackSound;

	private Vector3 destination;

	public delegate void selected(player p);

	public event selected isSelected;

	public int attack;

	public float attackRate;

	public int lifePoint;

	private GameObject target = null;

	public Vector3 Destination
	{
		get
		{
			return destination;
		}
		set
		{
			destination = value;
			float angle = Mathf.Atan((transform.position.y - Destination.y) / (transform.position.x - Destination.x));
			if (transform.position.x - Destination.x < 0 && transform.position.y - Destination.y > 0)
				angle =  3.14f + angle;
			else if (transform.position.x - Destination.x < 0 && transform.position.y - Destination.y < 0)
				angle =  - 3.14f + angle;
			angle = angle * 180.0f / 3.14f;
			animator.SetBool("stand", false);
			animator.SetBool("rigth", false);
			animator.SetBool("left", false);
			animator.SetBool("up", false);
			animator.SetBool("down", false);
			animator.SetBool("rigthUp", false);
			animator.SetBool("rigthDown", false);
			animator.SetBool("leftUp", false);
			animator.SetBool("leftDown", false);
			if (angle < 0)
			{
				if (angle < 0.0f && angle > -22.5f)
					animator.SetBool("left", true);
				else if (angle <= -22.5f && angle >= -67.5f)
					animator.SetBool("leftUp", true);
				else if (angle < -67.5f && angle > -112.5f)
					animator.SetBool("up", true);
				else if (angle <= -112.5f && angle >= -157.5f)
					animator.SetBool("rigthUp", true);
				else if (angle < -157.5f && angle >= -180.0f)
					animator.SetBool("rigth", true);
			}
			else
			{
				if (angle <= 180f && angle > 157.5f)
					animator.SetBool("rigth", true);
				else if (angle <= 157.5f && angle >= 112.5f)
					animator.SetBool("rigthDown", true);
				else if (angle < 112.5f && angle > 67.5f)
					animator.SetBool("down", true);
				else if (angle <= 67.5f && angle >= 22.5)
					animator.SetBool("leftDown", true);
				else if (angle < 22.5f && angle >= 0.0f)
					animator.SetBool("left", true);
			}
		}
	}

	AudioSource audioSource;

	void Start ()
	{
		audioSource = GetComponent<AudioSource>();
		Destination = transform.localPosition;
	}
	
	void attackTarget()
	{
		if (target.tag == "Orc")
		{
			orc orcEnnemies = target.GetComponent<orc>();
			orcEnnemies.lifePoint -= attack;
			print(orcEnnemies.name + " unit " + "[" + orcEnnemies.lifePoint + "/200]HP has been attacked.");
			if (orcEnnemies.lifePoint <= 0)
				animator.SetBool("stand", true);
		}
		else if (target.tag == "buildingOrc")
		{
			building orcBuild = target.GetComponent<building>();
			orcBuild.lifePoint -= attack;
			if (orcBuild.name == "orcTownCenter")
				print(orcBuild.name + " unit " + "[" + orcBuild.lifePoint + "/1000]HP has been attacked.");
			else
				print(orcBuild.name + " unit " + "[" + orcBuild.lifePoint + "/400]HP has been attacked.");
			if (orcBuild.lifePoint <= 0)
				animator.SetBool("stand", true);
		}
		attackRate = Time.time;
	}

	void Update ()
	{
		if (gameObject.transform.localPosition != Destination)
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.localPosition, Destination, speed);
		else
		{
			animator.SetBool("stand", true);
			animator.SetBool("rigth", false);
			animator.SetBool("left", false);
			animator.SetBool("up", false);
			animator.SetBool("down", false);
			animator.SetBool("rigthUp", false);
			animator.SetBool("rigthDown", false);
			animator.SetBool("leftUp", false);
			animator.SetBool("leftDown", false);
		}
		if (target != null && Time.time - attackRate > 2)
			attackTarget();
	}

	void OnMouseDown()
	{
		audioSource.Play();
		isSelected(this as player);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		float x = coll.transform.position.x - coll.gameObject.GetComponent<BoxCollider2D>().size.x / 2;
		float y = coll.transform.position.y - coll.gameObject.GetComponent<BoxCollider2D>().size.y / 2;
		Rect rect = new Rect(x, y, coll.gameObject.GetComponent<BoxCollider2D>().size.x, coll.gameObject.GetComponent<BoxCollider2D>().size.y);
        if (rect.Contains(Destination))
			Destination = gameObject.transform.localPosition;
		if (coll.gameObject.tag == "Orc" || coll.gameObject.tag == "buildingOrc")
		{
			audioSource.PlayOneShot(attackSound);
			target = coll.gameObject;
		}
		else
			return;
		float angle = Mathf.Atan((transform.position.y - target.transform.position.y) / (transform.position.x - target.transform.position.x));
		if (transform.position.x - target.transform.position.x < 0 && transform.position.y - target.transform.position.y > 0)
			angle =  3.14f + angle;
		else if (transform.position.x - target.transform.position.x < 0 && transform.position.y - target.transform.position.y < 0)
			angle =  - 3.14f + angle;
		angle = angle * 180.0f / 3.14f;
		animator.SetBool("stand", false);
		animator.SetBool("rigth", false);
		animator.SetBool("left", false);
		animator.SetBool("up", false);
		animator.SetBool("down", false);
		animator.SetBool("rigthUp", false);
		animator.SetBool("rigthDown", false);
		animator.SetBool("leftUp", false);
		animator.SetBool("leftDown", false);
		if (angle < 0)
		{
			if (angle < 0.0f && angle > -22.5f)
				animator.SetInteger("isAttacking", 6);
			else if (angle <= -22.5f && angle >= -67.5f)
				animator.SetInteger("isAttacking", 7);
			else if (angle < -67.5f && angle > -112.5f)
				animator.SetInteger("isAttacking", 0);
			else if (angle <= -112.5f && angle >= -157.5f)
				animator.SetInteger("isAttacking", 1);
			else if (angle < -157.5f && angle >= -180.0f)
				animator.SetInteger("isAttacking", 2);
		}
		else
		{
			if (angle <= 180f && angle > 157.5f)
				animator.SetInteger("isAttacking", 2);
			else if (angle <= 157.5f && angle >= 112.5f)
				animator.SetInteger("isAttacking", 3);
			else if (angle < 112.5f && angle > 67.5f)
				animator.SetInteger("isAttacking", 4);
			else if (angle <= 67.5f && angle >= 22.5)
				animator.SetInteger("isAttacking", 5);
			else if (angle < 22.5f && angle >= 0.0f)
				animator.SetInteger("isAttacking", 6);
		}
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		animator.SetInteger("isAttacking", 8);
	}
}
