using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMethodKey : MonoBehaviour
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

        GameObject.FindGameObjectsWithTag("RoomManager").getChild(1).PlayerHasSmallKey();
        // Find object Room Manager and launch verification
    }
}
