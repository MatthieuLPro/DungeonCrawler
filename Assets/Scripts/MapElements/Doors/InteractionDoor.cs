using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public int openMethod = 0;
    public bool open = true;
    [SerializeField]
    private Sprite _openSprite = null;
    [SerializeField]
    private Sprite _closeSprite = null;
    public int teleportDirection = 0;

    private SpriteRenderer _spriteRend;

    // teleportDirection = 0 => Up / 1 => Right / 2 => Down / 3 => Left
    // _openMethod = 0 (open) / = 1 (close key) / = 2 (close interrupteur) / = 3 (close Big Key)
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
