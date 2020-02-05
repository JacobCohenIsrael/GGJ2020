using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboSounds : MonoBehaviour
{
    public AudioClip[] footSteps;
    public AudioClip[] fixingSounds;
    public AudioClip[] dashSounds;
    public AudioClip[] hitSounds;

    public void PlayFootStep()
    {
        AudioSource.PlayClipAtPoint(footSteps[Random.Range(0, footSteps.Length)], transform.position);
    }

    public void PlayFixing()
    {
        AudioSource.PlayClipAtPoint(fixingSounds[Random.Range(0, fixingSounds.Length)], transform.position);
    }

    public void PlayDash()
    {
        AudioSource.PlayClipAtPoint(dashSounds[Random.Range(0, dashSounds.Length)], transform.position);
    }

    public void PlayHit()
    {
        AudioSource.PlayClipAtPoint(hitSounds[Random.Range(0, hitSounds.Length)], transform.position);
    }
}
