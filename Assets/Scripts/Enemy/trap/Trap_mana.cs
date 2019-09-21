using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_mana : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            Vector3 knockBackDir = (other.GetComponent<PlayerController>().GetPosition() - transform.position).normalized;
            player.DamageMana(knockBackDir, 1f, damage);
        }     
    }
}
