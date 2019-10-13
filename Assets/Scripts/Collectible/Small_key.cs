﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_key : MonoBehaviour
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
