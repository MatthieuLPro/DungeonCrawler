using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankUI : MonoBehaviour
{
    void Start()
    {
        RectTransform resultRect = GetComponent<RectTransform>();

        resultRect.localPosition = new Vector3(resultRect.localPosition.x, resultRect.localPosition.y + 50, resultRect.localPosition.z);
        Destroy(GetComponent<RankUI>());
    }
}
