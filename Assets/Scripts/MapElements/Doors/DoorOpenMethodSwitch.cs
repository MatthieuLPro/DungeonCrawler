using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMethodSwitch : MonoBehaviour
{
    [Header("Select the door to open")]
    [SerializeField]
    private GameObject[] _doors = null;

    /* ************************************************ */
    /* Toggle multiple when hero enter in switch collider */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        for(var i = 0; i < _doors.Length; i++)
        {
            Door TargetDoor = _doors[i].GetComponent<Door>();
            
            if (TargetDoor.open)
                TargetDoor.CloseDoor();
            else
                TargetDoor.OpenDoor();
        }
    }
}
