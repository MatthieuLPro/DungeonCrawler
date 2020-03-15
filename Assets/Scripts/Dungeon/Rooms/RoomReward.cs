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
        if (!door.GetComponent<BoxCollider2D>().enabled)
            return;

        door.GetComponent<BoxCollider2D>().enabled = false;
        door.GetComponent<SpriteRenderer>().sprite = door.GetComponent<Door>().openSprite;
        CallAudio("open");

    }

    /* Close door */
    public void CloseDoor(GameObject door)
    {
        if (door.GetComponent<BoxCollider2D>().enabled)
            return;

        door.GetComponent<BoxCollider2D>().enabled = true;
        door.GetComponent<SpriteRenderer>().sprite = door.GetComponent<Door>().closeSprite;
        CallAudio("close");
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
    
    /* ************************************************ */
    /* Treasures Consequences */
    /* ************************************************ */
    /* Treasure appear */
    /* TO DO */
    public void TreasureAppear(GameObject treasure){
        return;
    }


    /* ************************************************ */
    /* Event audio */
    /* ************************************************ */
    public void CallAudio(string roomEvent)
    {
        _SetAudioClip(roomEvent);
        _PlayAudioClip();
    }

    private void _SetAudioClip(string roomEvent){
        GetComponent<AudioSource>().clip = _NewAudioClip(roomEvent);
    }

    private void _PlayAudioClip(){
        GetComponent<AudioSource>().Play();
    }

    private AudioClip _NewAudioClip(string roomEvent)
    {
        AudioClip newAudioClip = null;
        switch(roomEvent)
        {
            case "door_open":
                newAudioClip = Resources.Load("Sounds/door_open") as AudioClip;
                break;
            case "door_close":
                newAudioClip =  Resources.Load("Sounds/door_close") as AudioClip;
                break;
            default:
                newAudioClip = Resources.Load("Sounds/door_close") as AudioClip;
                break;
        }
        return newAudioClip;
    }
}
