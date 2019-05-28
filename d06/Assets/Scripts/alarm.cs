using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarm : MonoBehaviour
{
    private Light ligth;

    public float blinkingRate;

    public float blinkingValue = 0;

    private bool ligthUp = true;

    void Start()
    {
        ligth = GetComponent<Light>();
    }

    void Update()
    {
        if (gameObject.name == "prop_megaphone")
        {
            if (progressBar.detectionPercent >= 100 && !GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else if (gameObject.name == "alarmLigth" && progressBar.detectionPercent >= 100)
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

        }
    }
}
