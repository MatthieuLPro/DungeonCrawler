using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMovements : NpcGeneralAddForces
{
    override public void AddForceMovement(Vector3 directionVariation){
        MoveObject(directionVariation);
    }
}
