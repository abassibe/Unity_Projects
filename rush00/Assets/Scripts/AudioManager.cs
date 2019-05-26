using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audio { get; private set; }
    public AudioClip music;
    private AudioSource source;

    void Start()
    {
        audio = this;
        source = GetComponent<AudioSource>();
        audio.AmbiancePlay();
    }

    void AmbiancePlay()
    {
        source.Play(0);
    }

    void AmbianceStop()
    {
        source.Stop();
    }

    public void playSound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
