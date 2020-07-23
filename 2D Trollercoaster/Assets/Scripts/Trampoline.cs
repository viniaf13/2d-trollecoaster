using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float bumpVelocity = 30f;
    [SerializeField] AudioClip bounceSFX = default;
    [SerializeField] float soundVolume = 0.1f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            bool hitAtTop = CheckForCollisionAtTop(other);
            if (hitAtTop)
            {
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb)
                {
                    Bounce(playerRb);
                }
            }
        }
    }

    private void Bounce(Rigidbody2D playerRb)
    {
        animator.SetTrigger(Constants.Animations.Touched);
        GameObject audioListener = GameObject.FindWithTag(Constants.Tags.AudioListener);
        AudioSource.PlayClipAtPoint(bounceSFX, audioListener.transform.position, soundVolume);
        playerRb.velocity = new Vector2(0f, bumpVelocity);
    }

    private bool CheckForCollisionAtTop(Collision2D other)
    {
        bool hitAtTop = false;
        foreach (ContactPoint2D point in other.contacts)
        {
            hitAtTop = (Mathf.Abs(point.normal.y) >= 0.9f);
        }
        return hitAtTop;
    }

}
