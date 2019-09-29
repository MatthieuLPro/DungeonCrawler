using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
    private SpriteRenderer _spriteRend;

    [Header("Treasure Sprite")]
    [SerializeField]
    private Sprite _open;

    private void Start(){
        _spriteRend = GetComponent<SpriteRenderer>();
    }

    public void OpenTheObject(){
        _spriteRend.sprite = _open;
        foreach(BoxCollider2D box in gameObject.GetComponents<BoxCollider2D>())
        {
            if (box.isTrigger == true)
                Destroy(box);
        }
    }
}
