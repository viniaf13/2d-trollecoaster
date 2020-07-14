using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] int scoreValue = 100;
    [SerializeField] float destroyDelay = 1f;

    [SerializeField] AudioClip fruitPickupSFX = default;
    [SerializeField] float soundVolume = 0.1f;

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            //TODO: send score to game session
            GetComponent<Animator>().SetTrigger(Constants.Animations.Collected);
            PlaySFX();
            Destroy(gameObject, destroyDelay);
        }
    }

    private void PlaySFX()
    {
        if (hasPlayed) return;
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(fruitPickupSFX, audioListener.transform.position, soundVolume);
        hasPlayed = true;
    }
}
