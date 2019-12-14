using UnityEngine;

public class FloorIce : Floor
{
    protected override void newMovement(TestMovement objectMovement)
    {
        objectMovement.iceFloor             = true;
        objectMovement.accelerationTemp     = .1f;
        objectMovement.deccelerationTemp    = .99f;
        objectMovement.maxSpeedTemp         *= 3;

        objectMovement.gameObject.transform.parent.GetComponent<Rigidbody2D>().angularDrag = .05f;
    }

    protected override void oldMovement(TestMovement objectMovement)
    {
        objectMovement.iceFloor             = false;
        objectMovement.accelerationTemp     = objectMovement.acceleration;
        objectMovement.deccelerationTemp    = objectMovement.decceleration;
        objectMovement.maxSpeedTemp         = objectMovement.maxSpeed;

        objectMovement.gameObject.transform.parent.GetComponent<Rigidbody2D>().angularDrag = 0;
    }
}
