using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [Header("Open Settings")]
    public bool open = true;
    public int openMethod = 0;

    [Header("Teleport Settings")]
    public int teleportDirection = 0;
    public float teleportDistance = 7.0f;

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
    
    /* OpenMethod:
            0 => Open
            1 => Small Key
            2 => Interruptor
            3 => Big Key
            4 => Enemys
    */

    void Start()
    {
        _spriteRend = GetComponent<SpriteRenderer>();

        if (openMethod > 0)
            open = false;
        else
            OpenDoor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (openMethod == 1 && other.gameObject.GetComponent<Player>().HasKey())
            OpenDoor();
            
        if (openMethod == 3 && other.gameObject.GetComponent<Player>().HasBigKey())
            OpenDoor();
    }

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
