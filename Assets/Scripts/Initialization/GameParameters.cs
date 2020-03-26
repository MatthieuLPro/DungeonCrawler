using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameters : MonoBehaviour
{
    static public int PlayersNumber = 4;

    public void SetPlayersNumber(int playersCount)
    {
        PlayersNumber = playersCount;
    }

    public int GetPlayersNumber()
    {
        return PlayersNumber;
    }
}
