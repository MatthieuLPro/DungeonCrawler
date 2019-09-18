using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            Vector3 knockBackDir = (player.GetPosition() - transform.position).normalized;
            player.DamageKnockBack(knockBackDir, 1f, damage);
        }     
    }
}
