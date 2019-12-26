using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInformation : MonoBehaviour
{
    [HideInInspector]
    public Vector2 roomLimitsMax;
    public Vector2 roomLimitsMin;

    void Start()
    {
        roomLimitsMin = new Vector2(-1.3f, -3.34f);
        roomLimitsMax = new Vector2(1.31f, 4.39f);
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
