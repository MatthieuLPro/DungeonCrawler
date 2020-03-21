using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransfert : MonoBehaviour
{
    [Header("New player position")]
    [SerializeField]
    private Vector2 _newPosition = Vector2.zero;

    [Header("Next room information")]
    [SerializeField]
    private string _nextRoomCoord = "00";
    [SerializeField]
    private string _nextRoomLevel = "0";

    [Header("Type of transition")]
    [SerializeField]
    private bool _teleportPlayer = false;
    // Transition = false || true (no teleport / yes teleport)
    [SerializeField]
    private bool _updateFogList = false;
    [SerializeField]
    private bool _isInside = false;
 
    private RoomInformation _nextRoomInformation;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    private void Awake(){
        _nextRoomInformation = transform.root.Find("Level_" + NextRoomLevel).Find("Room_" + NextRoomCoord).GetComponent<RoomInformation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameObject player                           = other.gameObject;
        Transform parent                            = player.transform.parent;
        ResultPlayer resultplayer                   = parent.GetComponent<ResultPlayer>();
        RoomPlayerInformation roomPlayerInfo        = parent.GetComponent<RoomPlayerInformation>();
        CameraController camController              = parent.transform.Find("Camera").GetComponent<CameraController>();

        if (TeleportPlayer)
            UpdatePlayerPosition(player, GetPlayerNewPosition(player));

        UpdatePlayerRoomInformation(roomPlayerInfo);
        UpdatePlayerRoomLimits(roomPlayerInfo);

        if (UpdateFogList)
            UpdatePlayerFogList(roomPlayerInfo);

        if (!_nextRoomInformation.AlreadyVisited)
            UpdatePlayerResult(resultplayer);

        UpdateCamera(camController);
    }

    public string NextRoomCoord {
        get { return _nextRoomCoord; }
        set { _nextRoomCoord = value; }
    }

    public string NextRoomLevel {
        get { return _nextRoomLevel; }
        set { _nextRoomLevel = value; }
    }

    public Vector2 NewPosition {
        get { return _newPosition; }
    }

    public bool TeleportPlayer {
        get { return _teleportPlayer; }
    }

    public bool UpdateFogList {
        get { return _updateFogList; }
    }

    public bool IsInside {
        get { return _isInside; }
    }

    /* ************************************************ */
    /* Get New position for teleport */
    /* ************************************************ */
    private Vector2 GetPlayerNewPosition(GameObject player)
    {
        Vector2 position;

        if (NewPosition.x == 0)
            position.x = player.transform.position.x;
        else
            position.x = NewPosition.x;

        if (NewPosition.y == 0)
            position.y = player.transform.position.y;
        else
            position.y = NewPosition.y;

        return position;
    }

    /* ************************************************ */
    /* Update */
    /* ************************************************ */
    /* Update player informations */
    private void UpdatePlayerPosition(GameObject player, Vector2 newPosition){
        player.transform.position = newPosition;
    }
    private void UpdatePlayerRoomInformation(RoomPlayerInformation roomPlayerInfo){
        roomPlayerInfo.ActualLevel              = NextRoomLevel;
        roomPlayerInfo.PreviousRoom             = roomPlayerInfo.ActualRoom;
        roomPlayerInfo.PreviousRoomIsInside     = roomPlayerInfo.ActualRoomIsInside;
        roomPlayerInfo.ActualRoom               = NextRoomCoord;
        roomPlayerInfo.ActualRoomIsInside       = IsInside;
    }

    private void UpdatePlayerRoomLimits(RoomPlayerInformation roomPlayerInfo) {
        roomPlayerInfo.PlayerRoomLimits = _nextRoomInformation.getRoomLimits();
    }

    private void UpdatePlayerFogList(RoomPlayerInformation roomPlayerInfo) {
        roomPlayerInfo.UpdateFogList(IsInside);
    }

    private void UpdatePlayerResult(ResultPlayer resultPlayer) {
        resultPlayer.EnterFirstInRoom(_nextRoomInformation.RoomValue);
        _nextRoomInformation.AlreadyVisited = true;
    }

    /* Update camera informations */
    private void UpdateCamera(CameraController camera){
        camera.updateMinMaxLimits();
    }
}
