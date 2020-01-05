using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    [Header("Door rules Settings")]
    public int openDoor = 0;

    [Header("Door Sprites")]
    public Sprite openSprite = null;
    public Sprite closeSprite = null;

    /*
        if _open_rule == 0
            _open_rule => RoomManager (switch or enemies)
        else
            if _open_rule => 1 : SmallKey
            if _open_rule => 2 : BigKey
     */

    private RoomManager roomManager;

    private void Start()
    {
        roomManager = transform.parent.transform.parent.transform.parent.GetComponent<RoomManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (openDoor == 0)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (roomManager.VerifyOneObject(other.gameObject, openDoor))
            roomManager.RewardOneObject(gameObject, 0);
    }
}
