using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    [Header("Room informations")]
    public Vector2  roomLimitsMin;
    public Vector2  roomLimitsMax;

    [Header("Room value")]
    [SerializeField]
    private int _roomValue = 10;
    private bool _alreadyVisited = false;

    public Vector2[] getRoomLimits(){
        return new Vector2[] { roomLimitsMin, roomLimitsMax };
    }

    /* Getter & Setter */
    public bool AlreadyVisited {
        get { return _alreadyVisited; }
        set { _alreadyVisited = true; }
    }
    public int RoomValue {
        get { return _roomValue; }
    }
}
