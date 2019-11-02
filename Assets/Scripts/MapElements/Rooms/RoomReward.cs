using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReward : MonoBehaviour
{    
    /* ************************************************ */
    /* Doors Consequences */
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
    
    /* ************************************************ */
    /* Enemis Consequences */
    /* ************************************************ */
    /* Enemy appear */
    public void EnemyAppear(GameObject enemy)
    {        
        enemy.transform.GetChild(0).gameObject.SetActive(true);
        enemy.transform.GetChild(1).gameObject.SetActive(true);
        enemy.GetComponent<SpriteRenderer>().enabled = true;
        enemy.GetComponent<BoxCollider2D>().enabled = true;
    }
}
