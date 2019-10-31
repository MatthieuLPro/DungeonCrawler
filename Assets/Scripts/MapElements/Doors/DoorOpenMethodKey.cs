using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMethodKey : Door
{   
    /* OpenMethodKey:
            1 => Small Key
            3 => Big Key
    */

    /* ************************************************ */
    /* Unlock door when hero enter in door collider ans has key */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player  player      = other.gameObject.GetComponent<Player>();
        Door    TargetDoor  = GetComponent<Door>();

        if ((TargetDoor.openMethod == 1 && player.HasKey()) || (TargetDoor.openMethod == 3 && player.HasBigKey()))
            TargetDoor.OpenDoor();
    }
}
