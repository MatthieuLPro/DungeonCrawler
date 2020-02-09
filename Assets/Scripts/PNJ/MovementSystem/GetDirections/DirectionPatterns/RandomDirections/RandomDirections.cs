using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ************************************************ */
/* Random directions */
/* ************************************************ */
/* If _enableHunt == false */
// New direction depending of target position
// Set a new random direction on X && Y
abstract public class RandomDirections : NpcGeneralDirections
{
    override protected Vector2 GetDirectionVariations(){
        return _GetDirection();
    }

    abstract protected Vector2 _GetDirection();
}
