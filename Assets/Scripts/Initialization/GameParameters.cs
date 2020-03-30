using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    static public int playersNumber = 1;
    static public int playersSpeed = 0;
    static public int consumablePresence = 0;
    static public int staticMonsterPresence = 0;

    public void SetGameParameters(int pNb, int pSp, int conP, int SMPr) {
        PlayersNumber           = pNb;
        PlayersSpeed            = pSp;
        ConsumablePresence      = conP;
        StaticMonsterPresence   = SMPr;
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
