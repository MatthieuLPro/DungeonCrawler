using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    [Header("Room informations")]
    public Vector2  roomLimitsMin;
    public Vector2  roomLimitsMax;

    public Vector2[] getRoomLimits(){
        return new Vector2[] { roomLimitsMin, roomLimitsMax };
    }
}
