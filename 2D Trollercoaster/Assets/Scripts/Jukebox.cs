using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] float soundVolume = 0.15f;
    [SerializeField] AudioClip backgroundMusic = default;
    [SerializeField] AudioClip successSound = default;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void StopSounds()
    {
        audioSource.Stop();
    }

    public void PlaySuccess()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
    }
}
