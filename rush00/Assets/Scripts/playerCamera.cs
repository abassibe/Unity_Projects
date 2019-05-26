using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{

    public GameObject hero;
    public float time;
    void Update()
    {
        time = (Time.time % 7) / 7f;
        Camera.main.backgroundColor = Color.HSVToRGB(time, 0.5f, 0.5f);
        if (hero)
            transform.position = new Vector3(
                hero.transform.position.x,
                hero.transform.position.y,
                -10f
            );
    }
}
