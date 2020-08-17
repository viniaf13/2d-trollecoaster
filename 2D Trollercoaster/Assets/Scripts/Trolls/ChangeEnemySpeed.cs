using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemySpeed : MonoBehaviour
{
    [SerializeField] int newSpeed = 6;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.ChangeMoveSpeed(newSpeed);
        }
    }
}
