using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float bumpVelocity = 30f;
    [SerializeField] AudioClip bounceSFX = default;
    [SerializeField] float soundVolume = 0.1f;

    private Animator animator;
    private float objectRotation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        objectRotation = transform.eulerAngles.z;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Player player = other.gameObject.GetComponent<Player>();
        //bool canPush = IsPushableLayer(other.gameObject);
        //if (player)
        //{
            bool hitAtTop = CheckForCollisionAtTop(other);
            if (hitAtTop)
            {
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb)
                {
                    Vector2 pushDirection = DefinePushDirection();
                    Bounce(playerRb, pushDirection);
                }
            }
        //}
    }

    private void Bounce(Rigidbody2D playerRb, Vector2 pushDirection)
    {
        animator.SetTrigger(Constants.Animations.Touched);
        GameObject audioListener = GameObject.FindWithTag(Constants.Tags.AudioListener);
        AudioSource.PlayClipAtPoint(bounceSFX, audioListener.transform.position, soundVolume);
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(pushDirection * bumpVelocity, ForceMode2D.Impulse);
    }

    private Vector2 DefinePushDirection()
    {
        Vector2 pushDirection = Vector2.zero;
        switch (objectRotation)
        {
            case 0f:
                pushDirection = Vector2.up;
                break;
            case 90f:
                pushDirection = Vector2.left;
                break;
            case 180f:
                pushDirection = Vector2.down;
                break;
            case 270f:
                pushDirection = Vector2.right;
                break;
            default:
                break;
        }
        return pushDirection;
    }

    private bool CheckForCollisionAtTop(Collision2D other)
    {
        float objectRotation = transform.eulerAngles.z;
        bool hitAtTop = false;
        foreach (ContactPoint2D point in other.contacts)
        {
            //Debug.DrawRay(point.point, point.normal, Color.red,2f);
            if (objectRotation == 0f || objectRotation == 180f)
            {
                hitAtTop = (Mathf.Abs(point.normal.y) >= 0.9f);
            }
            else if (objectRotation == 90f || objectRotation == 270f)
            {
                hitAtTop = (Mathf.Abs(point.normal.y) <= 0.9f);
            }
        }
        return hitAtTop;
    }
}
