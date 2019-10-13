using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    [Header("Select the door to open")]
    [SerializeField]
    private GameObject _door = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (_door.GetComponent<InteractionDoor>().openMethod != 2)
            return;

        if (_door.GetComponent<InteractionDoor>().open == false)
            _door.GetComponent<InteractionDoor>().OpenDoor();
        else
            _door.GetComponent<InteractionDoor>().CloseDoor();
    }
}
