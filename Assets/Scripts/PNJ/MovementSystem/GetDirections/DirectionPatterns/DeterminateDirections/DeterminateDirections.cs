using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DeterminateDirections : NpcGeneralDirections
{
    override protected Vector2 GetDirectionVariations(){
        return GetDirection();
    }

    abstract public Vector2 GetDirection();
}
