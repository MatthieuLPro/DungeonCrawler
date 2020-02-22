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

    private RoomReward _reward; 

    /*
        if _open_rule == 0
            _open_rule => RoomManager (switch or enemies)
        else
            if _open_rule => 1 : SmallKey
            if _open_rule => 2 : BigKey
    */

    void Start(){
        _reward = transform.parent.transform.parent.transform.parent.GetChild(1).GetComponent<RoomReward>();
    }

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
            _reward.OpenDoor(gameObject);
        }

        if (openDoor == 2 && player.HasBigKey()){
            _reward.OpenDoor(gameObject);
        }
    }
}
