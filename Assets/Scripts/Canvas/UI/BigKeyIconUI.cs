﻿using System.Collections;
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

        float side      = GetHorizontalSide(playerName);
        float xDistance = GetAdaptedDistance(true, side);
        float yDistance = GetAdaptedDistance(false, side);

        //SetRectLocalPosition(xDistance, yDistance);
    }

    public void UpdateIcon(bool hasBigKey) {
        Object[] sprites = Resources.LoadAll("Sprites/Objects/Keys/key_big");
        if (hasBigKey)
            _bigKeySprite.sprite = (Sprite)sprites[2];
        else
            _bigKeySprite.sprite = (Sprite)sprites[1];
    }

    // Get HUD Position
    float GetHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    float GetAdaptedDistance(bool isAxisX, float side = 0f) {
        if (isAxisX) {
            if (side == 1)
                return (1f / 1.55f) * side;
            else
                return (1f / 4.03f) * side;
        }
        return 1 / 1.8f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) * xDistance,
                                                (_cameraSize.y - _thickness) * yDistance,
                                                _resultRect.localPosition.z);
    }
}
