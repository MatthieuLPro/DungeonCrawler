using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bordersUI : MonoBehaviour
{
    public int NbOfPlayers = 1;
    public int playerIndex = 1;
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

        Debug.Log("_cameraSize: " + _cameraSize);
        Debug.Log("Screen width: " + Screen.width);
        Debug.Log("Screen height: " + Screen.height);

        SetPositionBorders();
        SetSizeBorders();
    }

    void SetPositionBorders()
    {
        float xPosition     = _cameraSize.x - _thickness / 2;
        float yPosition     = _cameraSize.y - _thickness / 2;
        float zPosition     = _borderTop.localPosition.z;

        if (NbOfPlayers == 1) {
            xPosition = _cameraSize.x - _thickness / 2;
            yPosition = _cameraSize.y - _thickness / 2;
        } else if (NbOfPlayers == 2) {
            if (playerIndex == 1) {
                xPosition = _cameraSize.x - _thickness / 2;
                yPosition = (_cameraSize.y - _thickness) / 2;
            } else if (playerIndex == 2) {
                xPosition = _cameraSize.x - _thickness / 2;
                yPosition = (_cameraSize.y - _thickness) / 2;
            }
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
        float xSize = _cameraSize.x * 2;
        float ySize = _cameraSize.y * 2;

        if (NbOfPlayers == 1) {
            xSize = _cameraSize.x * 2 - _thickness / 2;
            ySize = _cameraSize.y * 2 - _thickness / 2;
        } else if (NbOfPlayers == 2) {
            xSize = _cameraSize.x * 2 - _thickness / 2;
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
