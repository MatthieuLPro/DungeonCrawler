using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField]
    private float thrust = 0.2f;

    [SerializeField]
    private int _damagePower = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("ItemDestructible")){
            other.GetComponent<DestructibleItem>().Smash();
        }
    }
}
