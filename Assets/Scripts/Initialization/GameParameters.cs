using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    static public bool inDungeon            = false;
    static public int playersNumber         = 1;
    static public int playersSpeed          = 0;
    static public int consumablePresence    = 0;
    static public int staticMonsterPresence = 0;

    public delegate void initDelegate(GameObject go);
    public initDelegate m_initMethodGo;

    void Start() {
        if (InDungeon) {
            SetDungeon();
        }
    }

    public void SetGameParameters(int pNb, int pSp, int conP, int SMPr) {
        PlayersNumber           = pNb;
        PlayersSpeed            = pSp;
        ConsumablePresence      = conP;
        StaticMonsterPresence   = SMPr;
        InDungeon               = true;
    }

    public void SetDungeon() {
        GameObject go_rooms = transform.root.Find("Rooms").Find("Level_0").gameObject;

        if (ConsumablePresence == 0)
            m_initMethodGo = _DestroyGo;
        else
            m_initMethodGo = _SetActiveGo;
        _InitRule(go_rooms.transform, new string[] {"Items", "Consumables"}, m_initMethodGo);

        if (StaticMonsterPresence == 0)
            m_initMethodGo = _DestroyGo;
        else
            m_initMethodGo = _SetActiveGo;
        _InitRule(go_rooms.transform, new string[] {"Enemies", "Monsters"}, m_initMethodGo);

    }

    // For the moment go_position.size == 2 ONLY
    // Need to create recursive function for FIND
    void _InitRule(Transform tr_rooms, string[] go_position, initDelegate initMethod) {
        int length = tr_rooms.childCount;
        for(int i = 0; i < length; i++)
            initMethod(tr_rooms.GetChild(i).Find(go_position[0]).Find(go_position[1]).gameObject);
    }

    void _SetActiveGo(GameObject goToSetActive) {
        goToSetActive.SetActive(true);
    }
    void _DestroyGo(GameObject goToDestroy) {
        Destroy(goToDestroy);
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
