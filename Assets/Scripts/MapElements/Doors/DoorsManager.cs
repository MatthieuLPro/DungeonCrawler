using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManager : MonoBehaviour
{
    [Header("Add doors to manage")]
    [SerializeField]
    private GameObject[] _doors;
    
    [Header("Open Settings")]
    private int _openMethod = 0;

    void Start()
    {
        if (_openMethod > 0)
            CloseMultipleDoors();
        else
            OpenMultipleDoors();
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
