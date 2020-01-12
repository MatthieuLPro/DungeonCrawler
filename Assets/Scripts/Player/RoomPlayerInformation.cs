using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInformation : MonoBehaviour
{
    [Header("Actual player room information")]
    public string  actualRoom;
    public string  actualLevel;
    public Vector2 roomLimitsMax;
    public Vector2 roomLimitsMin;

    public string getActualRoom(){
        return actualRoom;
    }

    public string getActualLevel(){
        return actualLevel;
    }

    public Vector2[] getPlayerRoomLimits(){
        return new Vector2[] { roomLimitsMin, roomLimitsMax };
    }

    public void UpdateActualRoom(string newRoom){
        actualRoom = newRoom;
    }

    public void UpdateActualLevel(string newLevel){
        actualLevel = newLevel;
    }

    public void updatePlayerRoomLimits(Vector2[] newLimits)
    {
        roomLimitsMin = newLimits[0];
        roomLimitsMax = newLimits[1];
    }
}
