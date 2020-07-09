using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float bumpVelocity = 10f;
    [SerializeField] bool isBounceable = true;

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
                //Hit Enemy();         
            }
            else
            {
                player.Hit();
            }
        }
    }
}
