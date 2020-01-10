using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBlock : MonoBehaviour
{
    [Header("Set Teleport Arrival")]
    [SerializeField]
    private GameObject _teleportArrival = null;

    [Header("Min & max position for camera")]
    public Vector2 minPosition;
    public Vector2 maxPosition;

    [Header("Next room information")]
    public string nextRoomCoord;
    public string nextRoomLevel;
 
    private RoomInformation _nextRoomInformation;

    private void awake(){
        _nextRoomInformation = transform.root.Find("Level_" + nextRoomLevel).Find("Room_(" + nextRoomCoord + ")").GetComponent<RoomInformation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        GameObject player                       = other.gameObject;
        RoomPlayerInformation roomPlayerInfo    = player.GetComponent<RoomPlayerInformation>();
        CameraController camController          = player.transform.parent.transform.Find("Camera").GetComponent<CameraController>();

        player.GetComponent<Transform>().position = _teleportArrival.GetComponent<Transform>().position;
        roomPlayerInfo.updatePlayerRoomLimits(_nextRoomInformation.getRoomLimits());
        camController.updateMinMaxLimits();

    }
}
