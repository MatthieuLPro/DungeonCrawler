using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralDirections : MonoBehaviour
{
    private Vector2 _posVariation;
    public Vector2 PosVariation {
        get { return _posVariation; } 
        set { _posVariation = value; } 
    }

    public void UpdatePosition(){
        PosVariation = GetDirectionVariations();
    }

    abstract protected Vector2 GetDirectionVariations();
}
