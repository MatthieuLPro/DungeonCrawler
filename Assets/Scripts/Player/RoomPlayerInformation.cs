using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlayerInformation : MonoBehaviour
{
    [Header("Actual player room information")]
    [SerializeField]
    private string  _actualRoom = "00";
    [SerializeField]
    private string  _actualLevel = "0";
    private bool _actualRoomInside = false;
    public Vector2 roomLimitsMax;
    public Vector2 roomLimitsMin;
    public int playerNumber;

    private string _previousRoom = "00";
    private bool _previousRoomInside = false;

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

    public bool ActualRoomIsInside {
        get { return _actualRoomInside; }
        set { _actualRoomInside = value; }
    }

    public string PreviousRoom {
        get { return _previousRoom; }
        set { _previousRoom = value; }
    }

    public bool PreviousRoomIsInside {
        get { return _previousRoomInside; }
        set { _previousRoomInside = value; }
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

    public GameObject FogList {
        get { return _fogList; }
    }

    public GameObject UnFogList {
        get { return _unFogList; }
    }

    public void UpdateFogList(bool isInside) {
        if (ActualRoom == PreviousRoom && PreviousRoomIsInside == isInside)
            return;

        if (ActualRoom != PreviousRoom && PreviousRoomIsInside == false && isInside == true)
            UnFogObject();

        if (ActualRoom != PreviousRoom && PreviousRoomIsInside == true && isInside == false)
            FogObject();

        if (ActualRoom != PreviousRoom && PreviousRoomIsInside == isInside)
            return;
    }

    private void UnFogObject() {
        string newUnFog1    = string.Concat(ActualLevel, "-", ActualRoom, "-", PreviousRoom);
        string newUnFog2    = string.Concat(ActualLevel, "-", PreviousRoom, "-", ActualRoom);

        Transform newUnfog  = FogList.transform.Find(newUnFog1);
        if (newUnfog == null) {
            newUnfog = FogList.transform.Find(newUnFog2);
        }

        newUnfog.gameObject.layer     = 14;
        newUnfog.SetParent(UnFogList.transform);
    }

    private void FogObject() {
        if (UnFogList.transform.childCount == 0)
            return;
        GameObject newFog  = UnFogList.transform.GetChild(0).gameObject;
        newFog.layer       = 13;
        newFog.transform.SetParent(FogList.transform);
    }
}
