using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bordersUI : MonoBehaviour
{
    private Vector3 _cameraSize;

    private float _thickness;

    private RectTransform _borderTop;
    private RectTransform _borderRight;
    private RectTransform _borderBot;
    private RectTransform _borderLeft;

    void Start()
    {
        Camera camera = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>();
        _cameraSize = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);

        _borderTop   = transform.Find("top").gameObject.transform.GetComponent<RectTransform>();
        _borderRight = transform.Find("right").gameObject.transform.GetComponent<RectTransform>();
        _borderBot   = transform.Find("bot").gameObject.transform.GetComponent<RectTransform>();
        _borderLeft  = transform.Find("left").gameObject.transform.GetComponent<RectTransform>();

        _thickness = _borderTop.rect.height;

        SetPositionBorders();
        SetSizeBorders();
    }

    void SetPositionBorders()
    {
        float zPosition = _borderTop.localPosition.z;

        _borderTop.localPosition   = new Vector3(0,
                                                 (_cameraSize.y - _thickness) / 2,
                                                 zPosition);

        _borderBot.localPosition   = new Vector3(0,
                                                 (_cameraSize.y - _thickness) / 2 * -1f,
                                                 zPosition);

        _borderRight.localPosition = new Vector3((_cameraSize.x - _thickness) / 2,
                                                 0,
                                                 zPosition);

        _borderLeft.localPosition  = new Vector3((_cameraSize.x - _thickness) / 2 * -1f,
                                                 0,
                                                 zPosition);
    }

    void SetSizeBorders()
    {
        _borderTop.sizeDelta   = new Vector2(_cameraSize.x, _thickness);
        _borderBot.sizeDelta   = new Vector2(_cameraSize.x, _thickness);
        _borderRight.sizeDelta = new Vector2(_thickness, _cameraSize.y);
        _borderLeft.sizeDelta  = new Vector2(_thickness, _cameraSize.y);
    }
}
