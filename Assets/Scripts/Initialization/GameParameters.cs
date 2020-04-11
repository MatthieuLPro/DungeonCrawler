using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    public bool inDungeonTest = true;
    public int playerNumberTest = 0;
    public int playersSpeedTest = 0;
    public int consumablePresenceTest = 0;
    public int staticMonsterPresenceTest = 0;

    static public bool inDungeon            = false;
    static public int playersNumber         = 3;
    static public int playersSpeed          = 1;
    static public int consumablePresence    = 1;
    static public int staticMonsterPresence = 1;

    public delegate void initDelegate(GameObject go);
    public initDelegate m_initMethodGo;

    void Start() {
        SetGameParameters(playerNumberTest,
                          playersSpeedTest,
                          consumablePresenceTest,
                          staticMonsterPresenceTest);
        if (InDungeon) {
            SetDungeon();
        }
    }

    public int GetPlayersNumber() 
    {
        return playersNumber;
    }

    public void SetGameParameters(int pNb, int pSp, int conP, int SMPr) {
        PlayersNumber           = pNb;
        PlayersSpeed            = pSp;
        ConsumablePresence      = conP;
        StaticMonsterPresence   = SMPr;
        InDungeon               = true;
    }

    public void SetDungeon() {
        GameObject go_rooms     = transform.root.Find("Rooms").Find("Level_0").gameObject;
        GameObject go_players   = transform.root.Find("Players").gameObject;

        // Consumable
        if (ConsumablePresence == 0)
            m_initMethodGo = _DestroyGo;
        else
            m_initMethodGo = _SetActiveGo;
        _InitRoomRule(go_rooms.transform, new string[] {"Items", "Consumables"}, m_initMethodGo);

        // Monster
        if (StaticMonsterPresence == 0)
            m_initMethodGo = _DestroyGo;
        else
            m_initMethodGo = _SetActiveGo;
        _InitRoomRule(go_rooms.transform, new string[] {"Enemies", "Monsters"}, m_initMethodGo);


        // Player
        m_initMethodGo = _DestroyGo;
        _InitPlayerPresence(go_players.transform, m_initMethodGo);

        m_initMethodGo = _SetActiveGo;
        _InitPlayerRule(go_players.transform, m_initMethodGo);

        m_initMethodGo = _SetPlayerSpeed;
        _InitPlayerMovementRule(go_players.transform, new string[] {"Controller_", "Movement"}, m_initMethodGo);
    }

    // For the moment go_position.size == 2 ONLY
    // Need to create recursive function for FIND

    // Find a solution to facto initRoom/PLayer ?!
    void _InitRoomRule(Transform tr_rooms, string[] go_position, initDelegate initMethod) {
        int length = tr_rooms.childCount;
        for(int i = 0; i < length; i++) {
            if (tr_rooms.GetChild(i).gameObject.active)
                initMethod(tr_rooms.GetChild(i).Find(go_position[0]).Find(go_position[1]).gameObject);
        }
    }

    void _InitPlayerPresence(Transform tr_players, initDelegate initMethod) {
        if (PlayersNumber == 4)
            return;

        int length = Math.Min(tr_players.childCount, 4);
        for(int i = PlayersNumber; i < length; i++) {
            initMethod(tr_players.GetChild(i).gameObject);
        }
    }

    void _InitPlayerRule(Transform tr_players, initDelegate initMethod) {
        int length = Math.Min(tr_players.childCount, 4);
        for(int i = 0; i < length; i++) {
            initMethod(tr_players.GetChild(i).gameObject);
        }
    }

    void _InitPlayerMovementRule(Transform tr_players, string[] go_position, initDelegate initMethod) {
        int length = Math.Min(tr_players.childCount, 4);
        for(int i = 0; i < length; i++) {
            initMethod(tr_players.GetChild(i).Find(String.Concat(go_position[0], (i + 1).ToString())).Find(go_position[1]).gameObject);
        }
    }

    void _SetActiveGo(GameObject goToSetActive) {
        goToSetActive.SetActive(true);
    }
    void _DestroyGo(GameObject goToDestroy) {
        Destroy(goToDestroy);
    }

    void _SetPlayerSpeed(GameObject goMovement) {
        Movement playerMvt = goMovement.GetComponent<Movement>();
        if (PlayersSpeed == 0)
            playerMvt.MaxSpeed = 5;
        else if (PlayersSpeed == 1)
            playerMvt.MaxSpeed = 4;
        else
            playerMvt.MaxSpeed = 3;
    }

    public bool InDungeon {
        get { return inDungeon; }
        set { inDungeon = value; }
    }

    public int PlayersNumber {
        get { return playersNumber; }
        set { playersNumber = value; }
    }

    public int PlayersSpeed {
        get { return playersSpeed; }
        set { playersSpeed = value; }
    }

    public int ConsumablePresence {
        get { return consumablePresence; }
        set { consumablePresence = value; }
    }

    public int StaticMonsterPresence {
        get { return staticMonsterPresence; }
        set { staticMonsterPresence = value; }
    }
}
