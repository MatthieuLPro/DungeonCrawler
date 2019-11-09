using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGlue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.Find("MovementTest"))
            return;

        TestMovement objectMovement = other.transform.Find("MovementTest").GetComponent<TestMovement>();

        objectMovement.accelerationTemp = objectMovement.accelerationTemp / 2;
        objectMovement.maxSpeedTemp = objectMovement.maxSpeedTemp / 2;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.transform.Find("MovementTest"))
            return;

        TestMovement objectMovement = other.transform.Find("MovementTest").GetComponent<TestMovement>();

        objectMovement.accelerationTemp = objectMovement.acceleration;
        objectMovement.maxSpeedTemp = objectMovement.maxSpeed;
    }
}
