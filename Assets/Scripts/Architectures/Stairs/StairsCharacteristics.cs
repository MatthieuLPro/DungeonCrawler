using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCharacteristics : MonoBehaviour
{
    [Header("Settings Stairs")]
    public string sortLayerUp   = "Name of layer up";
    public string sortLayerDown = "Name of layer down";
    public int    layerUp       = 0;
    public int    layerDown     = 0;

    public string GetSortLayerUp(){
        return sortLayerUp;
    }

    public string GetSortLayerDown(){
        return sortLayerDown;
    }

    public int GetLayerUp(){
        return layerUp;
    }

    public int GetLayerDown(){
        return layerDown;
    }
}
