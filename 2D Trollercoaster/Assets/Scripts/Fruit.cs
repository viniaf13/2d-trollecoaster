using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            hasPickedUp = true;
            FindObjectOfType<ScoreDisplay>().AddToScore(scoreValue);
            GetComponent<Animator>().SetTrigger(Constants.Animations.Collected);
            PlaySFX();
            Destroy(gameObject, destroyDelay);
        }
    }

    private void PlaySFX()
    {
        GameObject audioListener = GameObject.FindWithTag(Constants.Tags.AudioListener);
        AudioSource.PlayClipAtPoint(fruitPickupSFX, audioListener.transform.position, soundVolume);
    }
}
