using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player)
        {
            FindObjectOfType<Jukebox>().PlaySuccess();
            GetComponent<Animator>().SetTrigger(Constants.Animations.Touched);
            player.GrantPlayerControl(false);
            player.SetPlayerVelocity(Vector2.zero);
            FindObjectOfType<CanvasController>().SwitchCanvas(2); //TODO: Fix magic number
        }
    }
}
