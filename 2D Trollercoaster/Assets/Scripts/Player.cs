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

    //State
    private bool isAlive = true;
    private bool playerControl = true;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private Collider2D mybodyCollider;
    private Collider2D myfeetCollider;
    private GameSession gameSession;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mybodyCollider = body.GetComponent<Collider2D>();
        myfeetCollider = feet.GetComponent<Collider2D>();
        gameSession = FindObjectOfType<GameSession>();
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
        myRigidBody.velocity = new Vector2(inputX, myRigidBody.velocity.y);

        bool playerHasXMovement = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasXMovement)
        {
            FlipSprite();
        }
    }
    private void Jump()
    {
        if (!IsLanded()) return;
        if (Input.GetButtonDown("Jump"))
        {
            //myRigidBody.velocity = new Vector2(0f, jumpForce);
            myRigidBody.AddForce(new Vector2 (0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
    }
    private bool IsLanded()
    {
        return myfeetCollider.IsTouchingLayers(LayerMask.GetMask(
            Constants.Layers.Ground, Constants.Layers.Enemies));
    }
    private void HandleAnimation()
    {
        if (!isAlive) { return; }
        bool playerHasYMovement = !IsLanded();
        myAnimator.SetBool(Constants.Animations.Landed, !playerHasYMovement);
        if (playerHasYMovement)
        {
            bool isHeJumping = (Mathf.Sign(myRigidBody.velocity.y) > 0);
            myAnimator.SetBool(Constants.Animations.Jump, isHeJumping);
        }
        else
        {
            bool playerHasXMovement = Mathf.Abs(myRigidBody.velocity.x) > Constants.Others.PlayerMovementThreshold;
            myAnimator.SetBool(Constants.Animations.Run, playerHasXMovement);
        }
    }
    private void Die()
    {
        isAlive = false;
        GrantPlayerControl(false);
        myAnimator.SetTrigger(Constants.Animations.Died);
        myRigidBody.bodyType = RigidbodyType2D.Static;
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
        myRigidBody.velocity = velocity;
    }
}
