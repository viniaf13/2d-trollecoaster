using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyToDynamic : MonoBehaviour
{
    [SerializeField] int newMass = 1;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponentInParent<Player>())
        {
            Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();
            myRigidBody.bodyType = RigidbodyType2D.Dynamic;
            myRigidBody.mass = newMass;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<Player>())
        {
            Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            myRigidBody.mass = newMass;
        }
    }
}
