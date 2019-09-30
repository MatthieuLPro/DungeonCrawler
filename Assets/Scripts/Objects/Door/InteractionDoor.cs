using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : MonoBehaviour
{
    [Header("Door parameter")]
    public int openMethod = 0;
    public bool open = true;
    [SerializeField]
    private Sprite _openSprite = null;
    [SerializeField]
    private Sprite _closeSprite = null;

    private SpriteRenderer _spriteRend;

    // _openMethod = 0 (open) / = 1 (close key) / = 2 (close interrupteur)
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
        
        if (openMethod == 1 && playerHasKey(other.gameObject))
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

    private bool playerHasKey(GameObject player)
    {
        if (player.GetComponent<Player>().keys <= 0)
            return false;

        player.GetComponent<Player>().keys--;   
        return (true);
    }
}
