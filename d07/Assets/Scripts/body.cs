using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class body : MonoBehaviour
{
    public float speed;

    public float rotate;

    public float boost;

    public int HP = 100;

    void FixedUpdate()
    {
        if (HP <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) && boost > 0)
            {
                transform.Translate(new Vector3(0, 0, speed * 2));
                boost -= 1f;
            }
            else
                transform.Translate(new Vector3(0, 0, speed));
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift) && boost > 0)
            {
                transform.Translate(new Vector3(0, 0, -(speed * 2)));
                boost -= 1f;
            }
            else
                transform.Translate(new Vector3(0, 0, -speed));
        }
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0, -rotate, 0);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0, rotate, 0);
        if (boost < 100)
            boost += 0.3f;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Gun Shoot(Clone)")
            HP -= 5;
        if (col.gameObject.name == "MissileShoot(Clone)")
            HP -= 30;
    }
}
