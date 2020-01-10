using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInformation : MonoBehaviour
{
    public Vector2 roomLimitsMax;
    public Vector2 roomLimitsMin;

    public void updatePlayerRoomLimits(Vector2[] newLimits)
    {
        roomLimitsMin = newLimits[0];
        roomLimitsMax = newLimits[1];
    }

    public Vector2[] getPlayerRoomLimits(){
        return new Vector2[] { roomLimitsMin, roomLimitsMax };
    }
}
