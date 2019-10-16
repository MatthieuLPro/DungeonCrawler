﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTunnel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        Destroy(other.gameObject);
    }
}
