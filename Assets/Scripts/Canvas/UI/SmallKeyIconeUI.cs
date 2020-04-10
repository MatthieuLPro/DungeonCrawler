using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallKeyIconeUI : MonoBehaviour
{
    // Position data
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;

    void Start()
    {
        Camera camera       = transform.parent.transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>();
        string playerName   = transform.parent.transform.parent.transform.parent.name;

        _cameraSize = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);
        _resultRect = GetComponent<RectTransform>();
        _thickness  = _resultRect.rect.height;

        float side      = GetHorizontalSide(playerName);
        float xDistance = GetAdaptedDistance(true, side);
        float yDistance = GetAdaptedDistance(false, side);

        //SetRectLocalPosition(xDistance, yDistance);
    }

    // Get HUD Position
    float GetHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    float GetAdaptedDistance(bool isAxisX, float side = 0f) {
        if (isAxisX)
            return 0.725f * side;
        return 0.7f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        int playerIndex     = transform.parent.transform.parent.transform.parent.GetComponent<Player>().PlayerIndex;
        int playersNumber   = transform.root.Find("GameParameters").GetComponent<GameParameters>().PlayersNumber;

        if (playerIndex == 1) {
            float xValue = _cameraSize.x;
            float yValue = _cameraSize.y;
            if (playersNumber > 1) {
                xValue /= 2;
                yValue /= 2;
            }
            _resultRect.localPosition = new Vector3((xValue - _thickness) * xDistance,
                                                    (yValue - _thickness) * yDistance,
                                                    _resultRect.localPosition.z);
        } else {
            SmallKeyIconeUI smallKeysIconeUi = transform.parent.transform.parent.transform.parent.transform.parent.Find("Player_1").Find("UI").Find("SmallKeys").Find("SmallKeyUI").GetComponent<SmallKeyIconeUI>();
            float xValue = smallKeysIconeUi.ResultRect.localPosition.x;
            if (playerIndex == 2 || playerIndex == 4)
                xValue *= -0.925f;

            _resultRect.localPosition = new Vector3(xValue,
                                                    smallKeysIconeUi.ResultRect.localPosition.y,
                                                    smallKeysIconeUi.ResultRect.localPosition.z);
        }
    }

    public RectTransform ResultRect {
        get { return _resultRect; }
    }
}
