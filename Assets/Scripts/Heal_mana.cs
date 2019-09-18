using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_mana : MonoBehaviour
{
    [SerializeField]
    private int _heal = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            player.HealMana(_heal);
            Destroy(gameObject);
        }     
    }
}
