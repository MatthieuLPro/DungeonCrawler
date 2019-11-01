using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Add doors to manage")]
    [SerializeField]
    private GameObject[] _doors;
    
    void Start()
    {
        //if (transform.GetChild(0).GetComponent<DoorOpenMethodEnemis>().VerificationEnemis())
        //    OpenMultipleDoors();
    }

    /* ************************************************ */
    /* Open close multiple doors */
    /* ************************************************ */
    /* Open multiple doors */
    public void OpenMultipleDoors()
    {
        for(var i = 0; i < _doors.Length; i++)
            _doors[i].GetComponent<Door>().OpenDoor();
    }

    /* Close multiple doors */
    public void CloseMultipleDoors()
    {
        for(var i = 0; i < _doors.Length; i++)
            _doors[i].GetComponent<Door>().CloseDoor();
    }
}
