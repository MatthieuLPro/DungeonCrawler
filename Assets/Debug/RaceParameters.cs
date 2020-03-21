using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceParameters : MonoBehaviour
{
    static public int PlayersCount;

    public void SetPlayersCount(int playersCount) 
    {
        PlayersCount = playersCount;
    }

    public int GetPlayersCount()
    {
        return PlayersCount;
    }
}
