using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class RandomDirectionLines : RandomDirections
{
    override protected Vector2 _GetDirection(){
        return _GetVariation();
    }

    abstract protected Vector2 _GetVariation();
}
