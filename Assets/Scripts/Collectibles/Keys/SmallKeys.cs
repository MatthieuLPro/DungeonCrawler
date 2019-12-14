﻿using UnityEngine;

public class SmallKeys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.GetSmallKey();
            Destroy(gameObject);
        }     
    }
}
