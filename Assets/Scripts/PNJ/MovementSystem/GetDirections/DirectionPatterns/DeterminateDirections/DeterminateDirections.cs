using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class DeterminateDirections : NpcGeneralDirections
{
    override protected Vector2 GetDirectionVariations(){
        return _GetDirection();
    }

    abstract protected Vector2 _GetDirection();
}
