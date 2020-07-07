using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    [Header("Body Elements")]
    [SerializeField] GameObject body = default;
    [SerializeField] GameObject feet = default;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private Collider2D bodyCollider;
    private Collider2D feetCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = body.GetComponent<Collider2D>();
        feetCollider = feet.GetComponent<Collider2D>();
    }

    void Update()
    {
        HandleAnimation();
        Move();
        Jump();
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

    private void HandleAnimation()
    {
        
        bool playerHasYMovement = !IsOnGround();
        myAnimator.SetBool("isLanded", !playerHasYMovement);
        if (playerHasYMovement)
        {
            bool isHeJumping = (Mathf.Sign(myRigidBody.velocity.y) > 0);
            myAnimator.SetBool("isJumping", isHeJumping);
        }
        else
        {
            bool playerHasXMovement = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            //bool playerHasXMovement = Mathf.Abs(myRigidBody.velocity.x) > 7f;
            myAnimator.SetBool("isRunning", playerHasXMovement);
        }
    }

    private void Jump()
    {
        if (!IsOnGround()) return;
        if (Input.GetButtonDown("Jump"))
        {
            myRigidBody.velocity = new Vector2(0f, jumpSpeed);
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
    }

    private bool IsOnGround()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
