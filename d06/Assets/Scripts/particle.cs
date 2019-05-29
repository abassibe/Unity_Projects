using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    ParticleSystem ps;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

    public GameObject cam;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        if (cam.transform.position.z < 215 && ps.name == "Particle System")
            return;
        else if (cam.transform.position.z > 215 && ps.name == "Particle System (1)")
            return;
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numStay = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        if (numEnter > 0 || numExit > 0 || numStay > 0)
            progressBar.isHide = true;
        else
            progressBar.isHide = false;
    }
}
