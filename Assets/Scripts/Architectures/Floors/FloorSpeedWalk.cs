using UnityEngine;

public class FloorSpeedWalk : Floor
{
    [Header("Floor SpeedWalk Settings")]
    [SerializeField]
    private float _thrust = 4.0f;
    [SerializeField]
    private Vector3 _forceDir;

    private void Awake(){
        _forceDir.Normalize();
    }

    protected override void newMovement(TestMovement objectMovement)
    {
        objectMovement.hasManyForce = true;
        objectMovement.maxSpeedTemp += _thrust;
        objectMovement.otherForce = _forceDir * _thrust;
    }

    protected override void oldMovement(TestMovement objectMovement)
    {
        objectMovement.hasManyForce = false;
        objectMovement.maxSpeedTemp = objectMovement.maxSpeed;
        objectMovement.otherForce = Vector3.zero;
    }
}
