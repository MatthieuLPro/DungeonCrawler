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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        GameObject player               = other.gameObject;
        RoomInformation roomInfo        = player.GetComponent<RoomInformation>();
        CameraController camController  = player.transform.parent.transform.Find("Camera").GetComponent<CameraController>();

        player.GetComponent<Transform>().position = _teleportArrival.GetComponent<Transform>().position;
        roomInfo.updateRoomLimits(minPosition, maxPosition);
        camController.updateMinMaxLimits();

    }
}
