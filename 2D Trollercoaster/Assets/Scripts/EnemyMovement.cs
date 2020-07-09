using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement stats")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Custom Options")]
    [SerializeField] bool isFacingRight = false;
    [SerializeField] bool isLimitedToEdges = true;

    private float moveDirection;
    private Rigidbody2D myRigidBody;
   
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        SetMoveDirection();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isLimitedToEdges) return;

        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        moveDirection *= (-1);
        myRigidBody.velocity = new Vector2 (moveSpeed * moveDirection, myRigidBody.velocity.y);
    }

    private void SetMoveDirection()
    {
        if (!myRigidBody) Debug.LogError("Rigidbody missing: " + gameObject.name);
        moveDirection = isFacingRight ? 1f : -1f;
        myRigidBody.velocity = new Vector2(moveSpeed * moveDirection, 0f);
        transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
    }
}
