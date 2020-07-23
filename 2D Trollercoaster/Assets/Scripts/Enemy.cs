using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int health = 1;
    [SerializeField] private float deathDelay = 0.7f;

    [Header("Movement Options")]
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private bool isLimitedToEdges = true;

    [Header("Player Bounce")]
    [SerializeField] private float bumpVelocity = 10f;
    [SerializeField] private bool isBounceable = true;

    [Header("SFX")]
    [SerializeField] float soundVolume = 0.1f;
    [SerializeField] AudioClip deathSFX = default;

    private float moveDirection;
    private Rigidbody2D myRigidBody;
    private bool isAlive = true;
    private GameObject audioListener;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        audioListener = GameObject.FindWithTag(Constants.Tags.AudioListener);
        SetMoveDirection();
    }

    //Handle hit methods
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            bool hitAtTop = false;
            foreach (ContactPoint2D point in other.contacts)
            {
                hitAtTop = (Mathf.Abs(point.normal.y) >= 0.9f);
            }
            if (hitAtTop && isBounceable)
            {
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb)
                {
                    playerRb.velocity = new Vector2(0f, bumpVelocity);
                }
                Hit();
            }
            else
            {
                player.Hit();
            }
        }
    }

    public void Hit()
    {
        health--;
        if (health <= 0 && isAlive)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        DisableEnemyColliders();
        AudioSource.PlayClipAtPoint(deathSFX, audioListener.transform.position, soundVolume);
        GetComponent<Animator>().SetTrigger(Constants.Animations.Died);
        Destroy(gameObject, deathDelay);
    }

    private void DisableEnemyColliders()
    {
        Destroy(myRigidBody);
        var childColliders = GetComponentsInChildren<Collider2D>();
        if (childColliders.Length < 1) return;
        foreach (Collider2D collider in childColliders)
        {
            Destroy(collider);
        }
    }

    //Movement methods
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isLimitedToEdges || !isAlive) return;

        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        moveDirection *= (-1);
        myRigidBody.velocity = new Vector2(moveSpeed * moveDirection, myRigidBody.velocity.y);
    }

    private void SetMoveDirection()
    {
        if (!myRigidBody) Debug.LogError("Rigidbody missing: " + gameObject.name);
        moveDirection = isFacingRight ? 1f : -1f;
        myRigidBody.velocity = new Vector2(moveSpeed * moveDirection, 0f);
        transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
    }
}