using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfert : MonoBehaviour
{
    [Header("New player position")]
    public Vector2 newPosition;

    [Header("Min & max position for camera")]
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameObject player               = other.gameObject;
        RoomInformation roomInfo        = player.GetComponent<RoomInformation>();
        CameraController camController  = player.transform.parent.transform.Find("Camera").GetComponent<CameraController>();
        Vector2 newPlayerPosition       = Vector2.zero;

        newPlayerPosition = updateValues(player);

        player.transform.position = new Vector2 (newPlayerPosition.x, newPlayerPosition.y);
        roomInfo.updateRoomLimits(minPosition, maxPosition);
        camController.updateMinMaxLimits();
    }

    private Vector2 updateValues(GameObject player)
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
