using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInformation : MonoBehaviour
{
    [Header("Actual player room information")]
    public string  _actualRoom;
    public string  _actualLevel;
    public Vector2 roomLimitsMax;
    public Vector2 roomLimitsMin;
    public int playerNumber;

    public GameObject _fogList;
    public GameObject _unFogList;

    void Start() {
        _fogList    = transform.Find("Fogs").gameObject;
        _unFogList  = transform.Find("Unfogs").gameObject;
    }

    /* Getter & Setter */
    public string ActualRoom {
        get { return _actualRoom; }
        set { _actualRoom = value; }
    }

    public string ActualLevel {
        get { return _actualLevel; }
        set { _actualLevel = value; }
    }

    public Vector2[] PlayerRoomLimits {
        get { return new Vector2[] { roomLimitsMin, roomLimitsMax}; }
        set { 
                roomLimitsMin = value[0];
                roomLimitsMax = value[1];
            }
    }

    public void UpdateFogList() {
        string newUnFog         = string.Concat(ActualLevel, "-", ActualRoom);
        GameObject unFogObject  = _unFogList.transform.GetChild(0).gameObject;
        GameObject fogObject    = _fogList.transform.Find(newUnFog).gameObject;

        unFogObject.layer   = 13;
        unFogObject.transform.SetParent(_fogList.transform);
        fogObject.layer     = 14;
        fogObject.transform.SetParent(_unFogList.transform);
    }
}
