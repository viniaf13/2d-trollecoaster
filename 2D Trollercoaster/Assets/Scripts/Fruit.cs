using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] int scoreValue = 100;
    [SerializeField] float destroyDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            //send score to game session
            GetComponent<Animator>().SetTrigger(Constants.Animations.Collected);
            Destroy(gameObject, destroyDelay);
        }
    }
}
