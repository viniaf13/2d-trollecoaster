using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSpeed : MonoBehaviour
{
    [SerializeField] float newMovespeed = 7f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            player.SetPlayerMovespeed(newMovespeed);
        }
    }
}
