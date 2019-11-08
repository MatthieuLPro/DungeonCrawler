using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorIce : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        TestMovement objectMovement = other.GetComponent<TestMovement>();

        objectMovement.iceFloor             = true;
        objectMovement.accelerationTemp     = .1f;
        objectMovement.deccelerationTemp    = .99f;
        objectMovement.maxSpeedTemp         *= 3;

        other.GetComponent<Rigidbody2D>().angularDrag = .05f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TestMovement objectMovement = other.GetComponent<TestMovement>();

        objectMovement.iceFloor             = false;
        objectMovement.accelerationTemp     = objectMovement.acceleration;
        objectMovement.deccelerationTemp    = objectMovement.decceleration;
        objectMovement.maxSpeedTemp         = objectMovement.maxSpeed;
        other.GetComponent<Rigidbody2D>().angularDrag = 0;
    }
}
