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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (openDoor == 0)
            return;

        if (!other.CompareTag("Player"))
            return;

        Player player = other.transform.parent.GetComponent<Player>(); 

        if (openDoor == 1 && player.HasSmallKey())
        {
            player.LooseSmallKey();
            OpenDoor();
        }

        if (openDoor == 2 && player.HasBigKey()){
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Door>().openSprite;
    }
}
