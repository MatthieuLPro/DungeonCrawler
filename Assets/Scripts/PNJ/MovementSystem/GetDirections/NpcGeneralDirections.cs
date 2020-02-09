using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralDirections : MonoBehaviour
{
    [HideInInspector]
    public Vector3 changePos;

    public Vector2 PosVariation { get; set; }

    private void FixedUpdate()
    {
        PosVariation = GetDirectionVariations();
        //MainController();
    }

    abstract protected Vector2 GetDirectionVariations();
}
