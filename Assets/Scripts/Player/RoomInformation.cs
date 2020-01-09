using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    [HideInInspector]
    public Vector2 roomLimitsMax;
    [HideInInspector]
    public Vector2 roomLimitsMin;

    void Awake()
    {
        roomLimitsMin = new Vector2(-8.58f, -7.47f);
        roomLimitsMax = new Vector2(8.56f, 8.48f);
    }

    public void updateRoomLimits(Vector2 newLimitsMin, Vector2 newLimitsMax)
    {
        roomLimitsMin.x = newLimitsMin.x;
        roomLimitsMin.y = newLimitsMin.y;
        roomLimitsMax.x = newLimitsMax.x;
        roomLimitsMax.y = newLimitsMax.y;
    }
    
    public Vector2[] getRoomLimits(){
        return new Vector2[] { roomLimitsMin, roomLimitsMax };
    }
}
