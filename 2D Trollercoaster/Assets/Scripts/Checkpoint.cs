using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player && !isActive)
        {
            isActive = true;
            GetComponent<Animator>().SetBool(Constants.Animations.Active, true);
            FindObjectOfType<GameSession>().SetLastCP(transform.position);
        }

    }

    public void DisableCP()
    {
        isActive = false;
        GetComponent<Animator>().SetBool(Constants.Animations.Active, false);
    }

}
