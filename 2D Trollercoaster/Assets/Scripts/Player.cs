using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float deathDelay = 1f;

    [Header("Body Elements")]
    [SerializeField] GameObject body = default;
    [SerializeField] GameObject feet = default;

    [Header("SFX")]
    [SerializeField] float soundVolume = 0.1f;
    [SerializeField] AudioClip playerDeathSFX = default;
    [SerializeField] AudioClip playerJumpSFX = default;

    [Header("Debug")]
    [SerializeField] bool restartFromCheckPoint = true;

    //State
    private bool isAlive = true;
    private bool playerControl = true;

    private Rigidbody2D rigidBody;
    private Animator animator;
    //private Collider2D mybodyCollider;
    private Collider2D feetCollider;
    private GameSession gameSession;
    private GameObject audioListener;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //mybodyCollider = body.GetComponent<Collider2D>();
        feetCollider = feet.GetComponent<Collider2D>();
        gameSession = FindObjectOfType<GameSession>();
        audioListener = GameObject.FindWithTag(Constants.Tags.AudioListener);

        if (restartFromCheckPoint && gameSession)
        {
            transform.position = gameSession.GetLastCP();
        }  
    }

    void Update()
    {
        if (!playerControl) { return; }
        Jump();  
    }

    private void FixedUpdate()
    {
        HandleAnimation();
        if (!playerControl) { return; }  
        Move();
    }

    private void Move()
    {
        float inputX = Input.GetAxis("Horizontal") * moveSpeed;

        //Desaccelarates when hit by something like a spring and then moves
        if (Mathf.Abs(rigidBody.velocity.x) > moveSpeed + Constants.Others.PlayerMovementThreshold) 
        { 
            if (Mathf.Sign(rigidBody.velocity.x) != Mathf.Sign(inputX))
            {
                rigidBody.velocity += new Vector2(inputX, 0f);
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(inputX, rigidBody.velocity.y);
        }

        bool playerHasXMovement = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasXMovement)
        {
            FlipSprite();
        }
    }

    private void Jump()
    {
        if (!IsGrounded()) return;
        if (Input.GetButtonDown("Jump"))
        {
            AudioSource.PlayClipAtPoint(playerJumpSFX, audioListener.transform.position, soundVolume);
            rigidBody.AddForce(new Vector2 (0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x),1f);
    }

    private bool IsGrounded()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask(
            Constants.Layers.Ground, Constants.Layers.Enemies, Constants.Layers.Interactables));
    }

    private void HandleAnimation()
    {
        if (!isAlive) { return; }
        bool playerHasYMovement = !IsGrounded();
        if (playerHasYMovement)
        {
            bool isHeJumping = (Mathf.Sign(rigidBody.velocity.y) > 0);
            animator.SetBool(Constants.Animations.Jump, isHeJumping);
        }
        else
        {
            bool playerHasXMovement = Mathf.Abs(rigidBody.velocity.x) > Constants.Others.PlayerMovementThreshold;
            animator.SetBool(Constants.Animations.Run, playerHasXMovement);
            animator.SetBool(Constants.Animations.Jump, false);
        }
        animator.SetBool(Constants.Animations.Landed, !playerHasYMovement);
    }

    private void Die()
    {
        if (!isAlive) {return; }
        isAlive = false;
        GrantPlayerControl(false);
        rigidBody.bodyType = RigidbodyType2D.Static;

        FindObjectOfType<Jukebox>().StopSounds();
        AudioSource.PlayClipAtPoint(playerDeathSFX, audioListener.transform.position, soundVolume);
        animator.SetTrigger(Constants.Animations.Died);

        gameSession.TakeLife();
        Destroy(gameObject, deathDelay);
    }

    public void Hit()
    {
        if (isAlive)
        {
            Die();
        }
    }

    public void GrantPlayerControl(bool control)
    {
        playerControl = control;
    }

    public void SetPlayerVelocity(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void SetPlayerMovespeed(float newMovespeed)
    {
        moveSpeed = newMovespeed;
    }
}
