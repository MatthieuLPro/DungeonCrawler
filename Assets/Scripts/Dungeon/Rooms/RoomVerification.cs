using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVerification : MonoBehaviour
{
    /* ************************************************ */
    /* Enemis conditions */
    /* ************************************************ */
    /* All enemis are dead */
    public bool EnemisDead(GameObject enemis){
        return (enemis == null);
    }

    /* ************************************************ */
    /* Floor Switch condition */
    /* ************************************************ */
    /* Switch */
    public bool FloorSwitch(GameObject switchObject){
        return switchObject.GetComponent<FloorSwitch>().value;
    }
    
    /* Toggle */
    public bool FloorToggle(GameObject switchObject){
        return switchObject.GetComponent<FloorToggle>().value;
    }

    /* ************************************************ */
    /* Player condition */
    /* ************************************************ */
    /* Player has at least 1 small key */
    public bool PlayerHasSmallKey(GameObject player){
        return player.GetComponent<Player>().keys > 0;
    }
    
    /* Player has big key */
    public bool PlayerHasBigKey(GameObject player){
        return player.GetComponent<Player>().bigKey;
    }
}
