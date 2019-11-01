using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVerification : MonoBehaviour
{
    /* ************************************************ */
    /* Enemis conditions */
    /* ************************************************ */
    /* All enemis are dead */
    public bool EnemisDead(GameObject[] enemis)
    {
        if (enemis.Length == 0)
            return true;

        return false;
    }

    /* ************************************************ */
    /* Player condition */
    /* ************************************************ */
    /* Player has at least 1 small key */
    public bool PlayerHasSmallKey(GameObject Player)
    {
        if (Player.GetComponent<Player>().keys > 0)
            return true;
        
        return false;
    }
    
    /* Player has big key */
    public bool PlayerHasBigKey(GameObject Player)
    {
        if (Player.GetComponent<Player>().bigKey)
            return true;
        
        return false;
    }
}
