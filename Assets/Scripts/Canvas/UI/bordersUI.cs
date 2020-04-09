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

        Destroy(GetComponent<bordersUI>());
    }

    void SetPositionBorders()
    {
        int playersNumber   = transform.root.Find("GameParameters").GetComponent<GameParameters>().PlayersNumber;
        int playerIndex     = transform.parent.transform.parent.GetComponent<Player>().PlayerIndex;

        float xPosition     = _cameraSize.x - _thickness / 2;
        float yPosition     = _cameraSize.y - _thickness / 2;
        float zPosition     = _borderTop.localPosition.z;

        if (playersNumber == 1) {
            xPosition = (_cameraSize.x - _thickness) / 2;
            yPosition = (_cameraSize.y - _thickness) / 2;
        } else if (playersNumber == 2) {
            xPosition = (_cameraSize.x - _thickness) / 2;
            yPosition = (_cameraSize.y - _thickness) / 2;
        } else {
            xPosition = (_cameraSize.x - _thickness) / 2;
            yPosition = (_cameraSize.y - _thickness) / 2;
        }

        _borderTop.localPosition   = new Vector3(0,
                                                 yPosition,
                                                 zPosition);

        _borderBot.localPosition   = new Vector3(0,
                                                 yPosition * -1f,
                                                 zPosition);

        _borderRight.localPosition = new Vector3(xPosition,
                                                 0,
                                                 zPosition);

        _borderLeft.localPosition  = new Vector3(xPosition * -1f,
                                                 0,
                                                 zPosition);
    }

    void SetSizeBorders()
    {
        float xSize         = _cameraSize.x * 2;
        float ySize         = _cameraSize.y * 2;
        int playersNumber   = transform.root.Find("GameParameters").GetComponent<GameParameters>().PlayersNumber;

        if (playersNumber == 1) {
            xSize = _cameraSize.x - _thickness / 2;
            ySize = _cameraSize.y - _thickness / 2;
        } else if (playersNumber == 2) {
            xSize = _cameraSize.x - _thickness / 2;
            ySize = _cameraSize.y - _thickness / 2;
        } else {
            xSize = _cameraSize.x - _thickness / 2;
            ySize = _cameraSize.y - _thickness / 2;
        }

        _borderTop.sizeDelta   = new Vector2(xSize, _thickness);
        _borderBot.sizeDelta   = new Vector2(xSize, _thickness);
        _borderRight.sizeDelta = new Vector2(_thickness, ySize);
        _borderLeft.sizeDelta  = new Vector2(_thickness, ySize);
    }
}
