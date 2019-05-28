using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkingLigth : MonoBehaviour
{
    private Light ligth;

    public float blinkingRate;
    public float blinkingValue = 0;
    private bool ligthUp = true;
    void Start()
    {
        ligth = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ligthUp)
            ligth.intensity += blinkingRate;
        else
            ligth.intensity -= blinkingRate;
        if (blinkingValue + 2 < Time.time)
        {
            blinkingValue = Time.time;
            ligthUp = !ligthUp;
        }
        if (ligth.intensity < 7)
            GetComponent<SphereCollider>().enabled = false;
        else
            GetComponent<SphereCollider>().enabled = true;

    }
}
