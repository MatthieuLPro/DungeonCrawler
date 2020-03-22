using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUI : MonoBehaviour
{
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;
    private Image _consumableObject = null;

    void Start()
    {
        Rect cameraPixelRect    = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>().pixelRect;
        string playerName       = transform.parent.transform.parent.name;

        _cameraSize         = new Vector3(cameraPixelRect.width, cameraPixelRect.height, 0);
        _resultRect         = GetComponent<RectTransform>();
        _thickness          = _resultRect.rect.height;
        _consumableObject   = transform.GetChild(0).GetComponent<Image>();

        _SetRectLocalPosition(_GetTextHorizontalSide(playerName));
    }

    public bool ConsumableExist() {
        return (_consumableObject.sprite != null);
    }

    public void AddConsumable(int consumable) {
        _consumableObject.sprite = _GetConsumableSprite(consumable);
        _consumableObject.color = new Color(255, 255, 255, 100);
    }

    public void RemoveConsumable() {
        _consumableObject.sprite = null;
        _consumableObject.color = new Color(255, 255, 255, 0);
    }


    Sprite _GetConsumableSprite(int consumable) {
        Object[] sprites = Resources.LoadAll("Sprites/Hud/consumables/object_consumable");
        if (consumable < 0 || consumable > 15)
            return null;
        return (Sprite)sprites[consumable];
    }

    // Get HUD Position
    float _GetTextHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    void _SetRectLocalPosition(float side = 1f) {
        _resultRect.localPosition = new Vector3(0,
                                                (_cameraSize.y - _thickness) / 1.2f,
                                                _resultRect.localPosition.z);
    }

}
