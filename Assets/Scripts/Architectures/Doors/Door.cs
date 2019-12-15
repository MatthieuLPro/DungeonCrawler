using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    [Header("Door Settings")]
    public int openDoor = 0;

    [Header("Door Sprites")]
    public Sprite openSprite = null;
    public Sprite closeSprite = null;

    /*
        if
            _open_rule => 1 : SmallKey
            _open_rule => 2 : BigKey
        else
            _open_rule => RoomManager
     */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (openDoor == 0)
            return;

        if (!other.CompareTag("Player"))
            return;

        RoomManager roomManager = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>();
        if (roomManager.VerifyOneObject(other.gameObject, openDoor))
            roomManager.RewardOneObject(gameObject, 0);
    }
}
