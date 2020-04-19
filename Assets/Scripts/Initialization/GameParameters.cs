using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameParameters : MonoBehaviour
{
    // Var to test
    public bool inDungeonTest = true;
    public int playerNumberTest = 0;
    public int playersSpeedTest = 0;
    public int consumablePresenceTest = 0;
    public int staticMonsterPresenceTest = 0;

    //
    static public bool inDungeon            = false;
    static public int playersNumber         = 3;
    static public int playersSpeed          = 1;
    static public int consumablePresence    = 1;
    static public int staticMonsterPresence = 1;

    public delegate void initDelegate(GameObject go);
    public initDelegate m_initMethodGo;

    // Uncomment to test without cutscene
    //void Awake() {
        //SetGameParameters(playerNumberTest,
        //                  playersSpeedTest,
        //                  consumablePresenceTest,
        //                  staticMonsterPresenceTest);
    //    if (InDungeon) {
    //        SetDungeon();
    //    }
    //}

    // Method to test
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
        //m_initMethodGo = _DestroyGo;
        //_InitPlayerPresence(go_players.transform, m_initMethodGo);

        //m_initMethodGo = _SetActiveGo;
        m_initMethodGo =_CreatePrefab;
        _InitPlayerRule(go_players.transform, m_initMethodGo);

        _InitScoreDungeon();

        //m_initMethodGo = _SetPlayerSpeed;
        //_InitPlayerMovementRule(go_players.transform, new string[] {"Controller_", "Movement"}, m_initMethodGo);
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

    /*void _InitPlayerPresence(Transform tr_players, initDelegate initMethod) {
        if (PlayersNumber == 4)
            return;

        int length = Math.Min(tr_players.childCount, 4);
        for(int i = PlayersNumber; i < length; i++) {
            initMethod(tr_players.GetChild(i).gameObject);
        }
    }*/

    void _InitPlayerRule(Transform tr_players, initDelegate initMethod) {
        int length = PlayersNumber;
        for(int i = 0; i < length; i++) {
            initMethod(tr_players.gameObject);
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

    // Only adapted for player prefab
    void _CreatePrefab(GameObject prefab) {
        GameObject prefabCustom         = Resources.Load("Prefabs/Players/Player_") as GameObject;
        int playerIndex                 = transform.root.transform.Find("Players").childCount - 1;
        GameObject newPrefab            = Instantiate(prefabCustom, Vector3.zero, Quaternion.identity);

        newPrefab.GetComponent<Player>().PlayerIndex = playerIndex;
        newPrefab.transform.Find("Controller_").GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Animations/Controller/Characters/PlayerLinkGreen") as RuntimeAnimatorController;
        newPrefab.transform.Find("Controller_").GetComponent<PlayerInput>().actions = Resources.Load(String.Concat("PlayerInput/PlayerInputs_", playerIndex.ToString())) as InputActionAsset;
        newPrefab.transform.Find("Controller_").name             = String.Concat("Controller_", playerIndex.ToString());
        newPrefab.transform.SetParent(transform.root.Find("Players").transform);
        newPrefab.transform.SetSiblingIndex(playerIndex - 1);
        newPrefab.name = String.Concat("Player_", playerIndex.ToString());
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

    void _InitScoreDungeon() {
        transform.root.Find("Players").Find("Scores").GetComponent<ScoresDungeon>().StartAfterPlayers();
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
