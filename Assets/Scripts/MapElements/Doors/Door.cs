using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [Header("Open Settings")]
    public int  openMethod = 0;

    [Header("Door Sprites")]
    [SerializeField]
    private Sprite _openSprite = null;
    [SerializeField]
    private Sprite _closeSprite = null;
    
    [HideInInspector]
    public bool open;
    
    /* OpenStat:
            0 => Open
            1 => Small Key
            2 => Interruptor
            3 => Big Key
            4 => Enemys
    */

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Awake()
    {
        if (openMethod > 0)
            open = false;
        else
            OpenDoor();
    }

    /* ************************************************ */
    /* Open Close unique door */
    /* ************************************************ */
    /* Open door */
    public void OpenDoor()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = _openSprite;
        open = true;
    }

    /* Close door */
    public void CloseDoor()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = _closeSprite;
        open = false;
    }
}
