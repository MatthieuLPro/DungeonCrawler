using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MvtSysDetection : MovementSystems
{
    /* Detector component */
    private ObjectDetector _detectorScript;

    protected override void Start()
    {
        base.Start();
        _directionVariation     = Vector3.zero;
        _npcGeneralDirection    = transform.GetChild(0).GetComponent<NpcGeneralDirections>();
        _npcGeneralAddForce     = transform.GetChild(1).GetComponent<NpcGeneralAddForces>();
        _detectorScript         = transform.parent.transform.GetChild(3).GetComponent<ObjectDetector>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_detectorScript.IsDetected) {
            _UpdateDirection();
            _UpdateMovement();
        }
    }
}
