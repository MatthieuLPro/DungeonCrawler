using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigKeyIconUI : MonoBehaviour
{
    private Image _bigKeySprite;

    void Start() {
        _bigKeySprite = GetComponent<Image>();
    }

    public void UpdateIcon(bool hasBigKey) {
        Object[] sprites = Resources.LoadAll("Sprites/Objects/Keys/key_big");
        if (hasBigKey)
            _bigKeySprite.sprite = (Sprite)sprites[2];
        else
            _bigKeySprite.sprite = (Sprite)sprites[1];
    }
}
