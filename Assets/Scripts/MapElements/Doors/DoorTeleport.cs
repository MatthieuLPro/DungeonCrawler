using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        other.GetComponent<Transform>().position = other.GetComponent<Transform>().position + FindTeleportPlace();
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        for(var i = 0; i < cameras.Length; i++)
        {
            if (camera[i].GetComponent<CameraController>().trackingGameObject == other.gameObject)
            {
                camera[i].GetComponent<Transform>().position = other.GetComponent<Transform>().position;
                camera[i].GetComponent<Transform>().position += new Vector3 (0, 0, -1f);
            }
        }
    }

    private Vector3 FindTeleportPlace()
    {
        Vector3 newPosition     = new Vector3(0, 0, 0);
        float teleportDistance  = transform.parent.GetComponent<InteractionDoor>().teleportDistance;

        switch(transform.parent.GetComponent<InteractionDoor>().teleportDirection)
        {
            case 0:
                newPosition += new Vector3(0, teleportDistance, 0);
                break;
            case 1:
                newPosition += new Vector3(teleportDistance, 0, 0);
                break;
            case 2:
                newPosition += new Vector3(0, teleportDistance, 0);
                break;
            default:
                newPosition += new Vector3(teleportDistance, 0, 0);
                break;
        }

        return (newPosition);
    }
}
