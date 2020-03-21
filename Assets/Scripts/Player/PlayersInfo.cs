using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInfo : MonoBehaviour
{

    public int PlayersCount = 1;
    private RoomPlayerInformation[] _playersInformation;

   
    void Start()
    {
        _playersInformation = new RoomPlayerInformation[4] { transform.GetChild(0).GetComponent<RoomPlayerInformation>(),
                                                             transform.GetChild(1).GetComponent<RoomPlayerInformation>(),
                                                             transform.GetChild(2).GetComponent<RoomPlayerInformation>(),
                                                             transform.GetChild(3).GetComponent<RoomPlayerInformation>() };
    }

    public bool isInRoom(string roomId)
    {
        for(int i = 0; i < _playersInformation.Length; i++)
        {
            if (_playersInformation[i].ActualRoom == roomId)
                return true;
        }
        return false;
    }

    private void _setCameras() 
    {

    }
}
