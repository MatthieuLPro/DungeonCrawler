using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWalk : MonoBehaviour
{
    [Header("SpeedWalk Settings")]
    [SerializeField]
    private float _thrust = .0f;
    [SerializeField]
    private Vector3 _vectorDirection;

    private Rigidbody2D _rb2d;

    private fixedUpdate()
    {
        if (_rb2d)
            AddAcceleration();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _rb2d = other.GetComponent<Rigidbody2D>();
        other.GetComponent<TestMovement>().hasManyForce = true;
        other.GetComponent<TestMovement>().maxSpeedTemp += _thrust;
        other.GetComponent<TestMovement>().AddAcceleration(_vectorDirection, _thrust);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<TestMovement>().hasManyForce = false;
        other.GetComponent<TestMovement>().maxSpeedTemp = other.GetComponent<TestMovement>().maxSpeed;
    }

    public void AddAcceleration(){
        _rb2d.AddForce(forceDir * thrust, ForceMode2D.Impulse);
    }
}
