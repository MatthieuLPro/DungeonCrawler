using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpOut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        GameObject player = other.gameObject;

        if(player.GetComponent<PlayerController>().maxSpeedTemp >= player.GetComponent<PlayerController>().maxSpeed)
            return;

        player.GetComponent<PlayerController>().maxSpeedTemp = player.GetComponent<PlayerController>().maxSpeed;
        player.GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.GetComponent<Stairs>().layoutUp;
        player.layer = transform.parent.GetComponent<Stairs>().layerUp;
    }
}
