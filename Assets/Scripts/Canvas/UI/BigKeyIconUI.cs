using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigKeyIconUI : MonoBehaviour
{
    // Position data
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;

    private Image _bigKeySprite;

    void Start()
    {
        Camera camera       = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>();
        string playerName   = transform.parent.transform.parent.name;

        _cameraSize     = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);
        _resultRect     = GetComponent<RectTransform>();
        _thickness      = _resultRect.rect.height;
        _bigKeySprite    = GetComponent<Image>();

        _SetRectLocalPosition(_resultRect.localPosition.z, _GetTextHorizontalSide(playerName));
        Destroy(GetComponent<RubyIconeUI>());
    }

    public void UpdateIcon(bool hasBigKey) {
        Object[] sprites = Resources.LoadAll("Sprites/Objects/Keys/key_big");
        if (hasBigKey)
            _bigKeySprite.sprite = (Sprite)sprites[2];
        else
            _bigKeySprite.sprite = (Sprite)sprites[1];
    }

    // Get HUD Position
    float _GetTextHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    void _SetRectLocalPosition(float zPosition, float side) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) / 4.3f * side,
                                                (_cameraSize.y - _thickness) / 2.3f,
                                                zPosition);
    }
}
