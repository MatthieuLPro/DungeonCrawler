using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        other.GetComponent<Transform>().position = other.GetComponent<Transform>().position + FindTeleportPlace();
    }

    private Vector3 FindTeleportPlace()
    {
        Vector3 newPosition = new Vector3(0, 0, 0);

        switch(transform.parent.GetComponent<InteractionDoor>().teleportDirection)
        {
            case 0:
                newPosition += new Vector3(0, 6.2f, 0);
                break;
            case 1:
                newPosition += new Vector3(6.2f, 0, 0);
                break;
            case 2:
                newPosition += new Vector3(0, -6.2f, 0);
                break;
            default:
                newPosition += new Vector3(-6.2f, 0, 0);
                break;
        }

        return (newPosition);
    }
}
