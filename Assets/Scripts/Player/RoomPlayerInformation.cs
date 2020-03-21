using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInformation : MonoBehaviour
{
    [Header("Actual player room information")]
    public string  _actualRoom = "00";
    public string  _actualLevel = "0";
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

    public void UpdateFogList(string previousRoom) {
        string newUnFog1        = string.Concat(ActualLevel, "-", ActualRoom, "-", previousRoom);
        string newUnFog2        = string.Concat(ActualLevel, "-", previousRoom, "-", ActualRoom);
        GameObject unFogObject  = _unFogList.transform.GetChild(0).gameObject;
        GameObject fogObject    = _fogList.transform.Find(newUnFog1).gameObject;
        if (fogObject == null) {
            fogObject = _fogList.transform.Find(newUnFog2).gameObject;
        }

        unFogObject.layer   = 13;
        unFogObject.transform.SetParent(_fogList.transform);
        fogObject.layer     = 14;
        fogObject.transform.SetParent(_unFogList.transform);
    }
}
