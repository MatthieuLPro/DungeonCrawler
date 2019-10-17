using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownOut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        GameObject player = other.gameObject;

        if(player.GetComponent<PlayerController>().speed >= 6)
            return;

        player.GetComponent<PlayerController>().speed = 6;
        player.GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.GetComponent<Stairs>().layoutDown;
        player.layer = transform.parent.GetComponent<Stairs>().layerDown;
    }
}
