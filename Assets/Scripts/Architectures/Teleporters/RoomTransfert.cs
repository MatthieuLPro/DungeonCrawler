using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfert : MonoBehaviour
{
    [Header("New player position")]
    public Vector2 newPosition;

    [Header("Next room information")]
    public string nextRoomCoord;
    public string nextRoomLevel;
 
    private RoomInformation _nextRoomInformation;

    private void Awake(){
        _nextRoomInformation = transform.root.Find("Level_" + nextRoomLevel).Find("Room_(" + nextRoomCoord + ")").GetComponent<RoomInformation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameObject player                           = other.gameObject;
        RoomPlayerInformation roomPlayerInfo        = player.transform.parent.GetComponent<RoomPlayerInformation>();
        CameraController camController              = player.transform.parent.transform.Find("Camera").GetComponent<CameraController>();
        Vector2 newPlayerPosition                   = GetPlayerNewPosition(player);

        player.transform.position = new Vector2 (newPlayerPosition.x, newPlayerPosition.y);
        roomPlayerInfo.updatePlayerRoomLimits(_nextRoomInformation.getRoomLimits());
        camController.updateMinMaxLimits();
    }

    private Vector2 GetPlayerNewPosition(GameObject player)
    {
        Vector2 position;

        if (newPosition.x == 0)
            position.x = player.transform.position.x;
        else
            position.x = newPosition.x;

        if (newPosition.y == 0)
            position.y = player.transform.position.y;
        else
            position.y = newPosition.y;

        return position;
    }
}
