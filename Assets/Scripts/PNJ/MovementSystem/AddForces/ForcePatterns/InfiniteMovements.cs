using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMovements : NpcGeneralAddForces
{
    override protected void AddForceMovement(){
        MoveObject();
    }
}
