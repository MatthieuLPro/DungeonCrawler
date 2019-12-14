using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    private float _teleportDistance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        other.GetComponent<Transform>().position = other.GetComponent<Transform>().position + FindTeleportPlace();
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        for(var i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].GetComponent<CameraController>().trackingGameObject == other.gameObject)
            {
                cameras[i].GetComponent<Transform>().position = other.GetComponent<Transform>().position;
                cameras[i].GetComponent<Transform>().position += new Vector3 (0, 0, -1f);
            }
        }
    }

    private Vector3 FindTeleportPlace()
    {
        Vector3 newPosition     = new Vector3(0, 0, 0);

        switch(_teleportDistance)
        {
            case 0:
                newPosition += new Vector3(0, _teleportDistance, 0);
                break;
            case 1:
                newPosition += new Vector3(_teleportDistance, 0, 0);
                break;
            case 2:
                newPosition += new Vector3(0, _teleportDistance, 0);
                break;
            default:
                newPosition += new Vector3(_teleportDistance, 0, 0);
                break;
        }

        return (newPosition);
    }
}
