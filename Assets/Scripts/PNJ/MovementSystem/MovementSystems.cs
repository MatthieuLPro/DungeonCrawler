using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystems : MonoBehaviour
{
    protected NpcGeneralDirections _npcGeneralDirection;
    protected NpcGeneralAddForces _npcGeneralAddForce;
    
    protected Vector3 _directionVariation;

    protected virtual void Start()
    {
        _directionVariation     = Vector3.zero;
        _npcGeneralDirection    = transform.GetChild(0).GetComponent<NpcGeneralDirections>();
        _npcGeneralAddForce     = transform.GetChild(1).GetComponent<NpcGeneralAddForces>();
    }

    protected virtual void FixedUpdate()
    {
        _UpdateDirection();
        _UpdateMovement();
    }

    protected void _UpdateDirection()
    {
        if (_npcGeneralAddForce._IsMoving)
            return;
        
        _npcGeneralDirection.UpdatePosition();
    }

    protected void _UpdateMovement()
    {
        _directionVariation = new Vector3(_npcGeneralDirection.PosVariation.x, _npcGeneralDirection.PosVariation.y, 0);
        _npcGeneralAddForce.AddForceMovement(_directionVariation);
    }
}
