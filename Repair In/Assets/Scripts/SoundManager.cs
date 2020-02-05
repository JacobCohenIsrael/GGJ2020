using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] blockHitSoundsList;
    private AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    public void PlayBlockHitSound()
    {
        myAudioSource.clip = blockHitSoundsList[Random.Range(0, blockHitSoundsList.Length)];
        myAudioSource.Play();
    }
}
