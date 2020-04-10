using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bordersUI : MonoBehaviour
{
    void Start()
    {
        RectTransform rectTr    = GetComponent<RectTransform>();
        Camera camera           = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>();
        Vector3 cameraSize      = new Vector3(1920, 1080, 0);
        Vector3 stageDimensions = new Vector2(Screen.width, Screen.height);

        string borderNameGO     = gameObject.name;
        float borderSize        = 0;
        float thickness         = 0;

        if (borderNameGO == "border_top" || borderNameGO == "border_bottom") {
            thickness = rectTr.rect.height;
            borderSize = cameraSize.x - thickness / 2;
        }
        else {
            thickness = rectTr.rect.width;
            borderSize = cameraSize.y - thickness / 2;
        }

        _SetSizeBorders(borderNameGO, borderSize, thickness);
        Destroy(GetComponent<bordersUI>());
    }

    void _SetSizeBorders(string nameGO, float size, float thickness)
    {
        string borderNameGO     = nameGO;
        float borderSize        = size;
        float borderThickness   = thickness;

        if (borderNameGO == "border_top" || borderNameGO == "border_bottom")
            GetComponent<RectTransform>().sizeDelta = new Vector2(borderSize, borderThickness);
        else
            GetComponent<RectTransform>().sizeDelta = new Vector2(borderThickness, borderSize);
    }
}
