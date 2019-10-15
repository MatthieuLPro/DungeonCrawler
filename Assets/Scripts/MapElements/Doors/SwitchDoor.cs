using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    [Header("Select the door to open")]
    [SerializeField]
    private GameObject[] _doors = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        for(var i = 0; i < _doors.Length; i++)
        {
            if (_doors[i].GetComponent<InteractionDoor>().openMethod != 2)
                return;
            
            if (_doors[i].GetComponent<InteractionDoor>().open == false)
                _doors[i].GetComponent<InteractionDoor>().OpenDoor();
            else
                _doors[i].GetComponent<InteractionDoor>().CloseDoor();
        }
    }
}
