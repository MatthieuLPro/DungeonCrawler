using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceParameters : MonoBehaviour
{
    static public int PlayersNumber = 1;

    public void SetPlayersNumber(int playersCount) 
    {
        PlayersNumber = playersCount;
    }

    public int GetPlayersNumber()
    {
        return PlayersNumber;
    }
}
