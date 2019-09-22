using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_mana : MonoBehaviour
{
    [SerializeField]
    private int _heal = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.GetMana(_heal);
            Destroy(gameObject);
        }     
    }
}
