using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            Vector3 knockBackDir = (player.GetPosition() - transform.position).normalized;
        }
    }
}
