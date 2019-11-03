using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        GameObject player = other.gameObject;
        player.GetComponent<PlayerController>().maxSpeedTemp = player.GetComponent<PlayerController>().maxSpeed / 2;
        player.GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.GetComponent<Stairs>().layoutUp;
    }
}
