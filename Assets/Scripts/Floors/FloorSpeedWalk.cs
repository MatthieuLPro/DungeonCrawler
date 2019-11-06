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
        _rb2d = other.GetComponent<Rigidbody2D>();
        other.GetComponent<TestMovement>().hasManyForce = true;
        other.GetComponent<TestMovement>().maxSpeedTemp += _thrust;
        other.GetComponent<TestMovement>().otherForce = _forceDir * _thrust;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _rb2d = null;
        other.GetComponent<TestMovement>().hasManyForce = false;
        other.GetComponent<TestMovement>().maxSpeedTemp = other.GetComponent<TestMovement>().maxSpeed;
        other.GetComponent<TestMovement>().otherForce = Vector3.zero;
    }
}
