using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Open Settings")]
    public bool open = true;
    public int openMethod = 0;

    [Header("Door Sprites")]
    [SerializeField]
    private Sprite _openSprite = null;
    [SerializeField]
    private Sprite _closeSprite = null;

    private SpriteRenderer _spriteRend;

    /* TeleportDirection:
            0 => Up
            1 => Right
            2 => Down
            3 => Left
    */
    
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
        _spriteRend = GetComponent<SpriteRenderer>();

        if (openMethod > 0)
            open = false;
        else
            OpenDoor();
    }

    /* ************************************************ */
    /* Open Close unique door */
    /* ************************************************ */
    public void OpenDoor()
    {
        _spriteRend.sprite = _openSprite;
        foreach (var box in GetComponents<BoxCollider2D>())
            box.enabled = false;
        open = true;
    }

    public void CloseDoor()
    {
        _spriteRend.sprite = _closeSprite;
        foreach (var box in GetComponents<BoxCollider2D>())
            box.enabled = true;
        open = false;
    }
}
