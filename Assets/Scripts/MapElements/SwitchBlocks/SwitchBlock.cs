using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    [Header("Block Sprites")]
    [SerializeField]
    private Sprite _downSprite = null;
    [SerializeField]
    private Sprite _upSprite = null;

    private BoxCollider2D _box = null;
    private SpriteRenderer _spriteRend = null;

    private void Start(){
        _box = GetComponent<BoxCollider2D>();
        _spriteRend = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(bool down)
    {
        if (down)
            _spriteRend.sprite = _downSprite;
        else
            _spriteRend.sprite = _upSprite;

        _box.enabled = !down;
    }
}
