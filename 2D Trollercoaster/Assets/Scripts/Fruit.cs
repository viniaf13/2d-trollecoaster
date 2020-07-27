using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Fruit : MonoBehaviour
{
    [SerializeField] int scoreValue = 10;
    [SerializeField] float destroyDelay = 1f;

    [SerializeField] AudioClip fruitPickupSFX = default;
    [SerializeField] float soundVolume = 0.1f;

    private bool hasPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player && !hasPickedUp)
        {
            PickUpFruit();
            PlaySFX();
            Destroy(gameObject, destroyDelay);
        }
    }

    private void PickUpFruit()
    {
        hasPickedUp = true;
        ScoreDisplay scoreDisplay = FindObjectOfType<ScoreDisplay>();
        if (scoreDisplay)
        {
            scoreDisplay.AddToScore(scoreValue);
        }
        GetComponent<Animator>().SetTrigger(Constants.Animations.Collected);
    }

    private void PlaySFX()
    {
        GameObject audioListener = GameObject.FindWithTag(Constants.Tags.AudioListener);
        AudioSource.PlayClipAtPoint(fruitPickupSFX, audioListener.transform.position, soundVolume);
    }
}
