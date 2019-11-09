using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpeedWalk : MonoBehaviour
{
    [Header("Floor SpeedWalk Settings")]
    [SerializeField]
    private float _thrust = 4.0f;
    [SerializeField]
    private Vector3 _forceDir;

    private Rigidbody2D _rb2d = null;

    private void Awake(){
        _forceDir.Normalize();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.Find("MovementTest"))
            return;

        TestMovement objectMovement = other.transform.Find("MovementTest").GetComponent<TestMovement>();

        _rb2d = other.GetComponent<Rigidbody2D>();
        objectMovement.hasManyForce = true;
        objectMovement.maxSpeedTemp += _thrust;
        objectMovement.otherForce = _forceDir * _thrust;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.Find("MovementTest"))
            return;

        TestMovement objectMovement = other.transform.Find("MovementTest").GetComponent<TestMovement>();

        _rb2d = null;
        objectMovement.hasManyForce = false;
        objectMovement.maxSpeedTemp = objectMovement.maxSpeed;
        objectMovement.otherForce = Vector3.zero;
    }
}
