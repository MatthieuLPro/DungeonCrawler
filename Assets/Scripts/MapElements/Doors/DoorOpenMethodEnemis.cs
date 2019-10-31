using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMethodEnemis : MonoBehaviour
{
    [Header("Select the door to open")]
    [SerializeField]
    private GameObject[] _doors = null;
    
    [Header("Select the enemis to kill")]
    [SerializeField]
    private GameObject[] _enemis = null;


    /* ************************************************ */
    /* Open close multiple doors */
    /* ************************************************ */
    /* Open multiple doors */
    private void OpenMultipleDoors()
    {
        for(var i = 0; i < _doors.Length; i++)
            _doors[i].GetComponent<Door>().OpenDoor();
    }

    /* Close multiple doors */
    private void CloseMultipleDoors()
    {
        for(var i = 0; i < _doors.Length; i++)
            _doors[i].GetComponent<Door>().CloseDoor();
    }
}
