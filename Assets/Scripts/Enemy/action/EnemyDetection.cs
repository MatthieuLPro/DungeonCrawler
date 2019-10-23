using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        Rigidbody2D player = other.GetComponent<Rigidbody2D>();
        if (player == null)
            return;

        if (transform.GetChild(0).transform.GetComponent<EnemyMovement>()._isWaiting)
            transform.GetChild(0).transform.GetComponent<EnemyMovement>().WakeEnemy();
    }
}
