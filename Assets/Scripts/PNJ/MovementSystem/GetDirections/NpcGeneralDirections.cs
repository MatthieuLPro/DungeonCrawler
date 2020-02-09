using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralDirections : MonoBehaviour
{
    public Vector2 PosVariation { get; set; }

    public void UpdatePosition(){
        PosVariation = GetDirectionVariations();
    }

    abstract protected Vector2 GetDirectionVariations();
}
