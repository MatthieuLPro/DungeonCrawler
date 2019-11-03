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

    private void OnTriggerEnter2D(Collider2D other){
        if (!other.GetComponent<PlayerController>().hasManyForce)
        {
            other.GetComponent<PlayerController>().hasManyForce = true;
            other.GetComponent<PlayerController>().maxSpeedTemp += _thrust;
        }
        other.GetComponent<Rigidbody2D>().AddForce(_vectorDirection * _thrust, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D other){
        other.GetComponent<PlayerController>().hasManyForce = false;
        other.GetComponent<PlayerController>().maxSpeedTemp = other.GetComponent<PlayerController>().maxSpeed;
        other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
