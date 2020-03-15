using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntDirectionDiagonals : HuntDirections
{
    override public Vector2 GetDirection(Vector2 targetPosition, Vector2 playerColliderSize)
    {
        float xPosition = _GetDirectionVariation(targetPosition.x, playerColliderSize.y / 2);
        float yPosition = _GetDirectionVariation(targetPosition.y, playerColliderSize.x / 2);

        return new Vector2(xPosition, yPosition);
    }
}
