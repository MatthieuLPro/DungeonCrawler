using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUI : MonoBehaviour
{
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;

    void Start()
    {
        Rect cameraPixelRect    = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>().pixelRect;
        string playerName       = transform.parent.transform.parent.name;

        _cameraSize = new Vector3(cameraPixelRect.width, cameraPixelRect.height, 0);

        int playerIndex     = transform.parent.transform.parent.GetComponent<Player>().PlayerIndex;

        _resultRect = GetComponent<RectTransform>();
        _thickness  = _resultRect.rect.height;

        float side      = GetHorizontalSide(playerName);
        float xDistance = GetAdaptedDistance(true, side);
        float yDistance = GetAdaptedDistance(false, side);

        SetRectLocalPosition(xDistance, yDistance);

        Destroy(GetComponent<RankUI>());
    }

    float GetHorizontalSide(string player) {
        int playersNumber = transform.root.Find("GameParameters").GetComponent<GameParameters>().PlayersNumber;
        if (player == "Player_1" || player == "Player_3" || playersNumber == 2)
            return -1f;
        return 1f;
    }

    float GetAdaptedDistance(bool isAxisX, float side = 0f) {
        if (isAxisX) {
            return (1f / 2.2f) * side;
        }
        return (1 / 2f) * -1f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        int playerIndex     = transform.parent.transform.parent.GetComponent<Player>().PlayerIndex;

        if (playerIndex == 1) {
            _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) * xDistance,
                                                    (_cameraSize.y - _thickness) * yDistance,
                                                    _resultRect.localPosition.z);
        } else {
            RankUI rankUi = transform.parent.transform.parent.transform.parent.Find("Player_1").Find("UI").Find("Rank").GetComponent<RankUI>();
            float xValue = rankUi.ResultRect.localPosition.x;
            if (playerIndex == 2 || playerIndex == 4)
                xValue *= -1;

            _resultRect.localPosition = new Vector3(xValue,
                                                    rankUi.ResultRect.localPosition.y,
                                                    rankUi.ResultRect.localPosition.z);
        }
    }

    public RectTransform ResultRect {
        get { return _resultRect; }
    }
}
