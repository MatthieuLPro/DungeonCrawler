using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystems : MonoBehaviour
{
    private NpcGeneralDirections _npcGeneralDirection;
    private NpcGeneralAddForces _npcGeneralAddForce;
    
    private Vector3 _directionVariation;

    void Start()
    {
        _directionVariation     = Vector3.zero;
        _npcGeneralDirection    = transform.GetChild(0).GetComponent<NpcGeneralDirections>();
        _npcGeneralAddForce     = transform.GetChild(1).GetComponent<NpcGeneralAddForces>();
    }

    void FixedUpdate()
    {
        _UpdateDirection();
        _UpdateMovement();
    }

    private void _UpdateDirection(){
        _npcGeneralDirection.UpdatePosition();
    }

    private void _UpdateMovement()
    {
        _directionVariation = new Vector3(_npcGeneralDirection.PosVariation.x, _npcGeneralDirection.PosVariation.y, 0);
        _npcGeneralAddForce.AddForceMovement(_directionVariation);
    }
}
