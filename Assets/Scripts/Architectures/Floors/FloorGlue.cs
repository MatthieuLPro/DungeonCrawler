public class FloorGlue : Floor
{
    protected override void newMovement(Movement objectMovement)
    {
        objectMovement.accelerationTemp = objectMovement.accelerationTemp / 2;
        objectMovement.maxSpeedTemp = objectMovement.maxSpeedTemp / 2;
    }

    protected override void oldMovement(Movement objectMovement)
    {
        objectMovement.accelerationTemp = objectMovement.acceleration;
        objectMovement.maxSpeedTemp = objectMovement.maxSpeed;
    }
}
