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
            GetComponent<Animator>().SetTrigger(Constants.Animations.Touched);
            player.GrantPlayerControl(false);
            player.SetPlayerVelocity(new Vector2(0f, 0f));
            FindObjectOfType<LevelLoader>().RestartLevel();
        }
    }
}
