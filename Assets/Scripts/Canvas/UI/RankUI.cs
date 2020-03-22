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
        Camera camera       = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>();
        string playerName   = transform.parent.transform.parent.name;

        _cameraSize = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);
        _resultRect = GetComponent<RectTransform>();
        _thickness  = _resultRect.rect.height;

        SetRectLocalPosition(_resultRect.localPosition.z, GetTextHorizontalSide(playerName));
        Destroy(GetComponent<RankUI>());
    }

    float GetTextHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    void SetRectLocalPosition(float zPosition, float side) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) / 2.2f * side,
                                                (_cameraSize.y - _thickness) / 2f * -1f,
                                                zPosition);
    }
}
