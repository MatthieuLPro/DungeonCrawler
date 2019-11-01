using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReward : MonoBehaviour
{    
    /* ************************************************ */
    /* Doors success */
    /* ************************************************ */
    /* Open door */
    public void OpenDoor(GameObject door)
    {
        door.GetComponent<BoxCollider2D>().enabled = false;
        door.GetComponent<SpriteRenderer>().sprite = door.GetComponent<Door>().openSprite;
    }

    /* Close door */
    public void CloseDoor(GameObject door)
    {
        door.GetComponent<BoxCollider2D>().enabled = true;
        door.GetComponent<SpriteRenderer>().sprite = door.GetComponent<Door>().closeSprite;
    }
}
