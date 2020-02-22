using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntDirectionLines : HuntDirections
{
    override protected Vector2 _GetDirection(Vector2 targetPosition, Vector2 playerColliderSize)
    {
        float xPosition = 0.0f;
        float yPosition = 0.0f;

        if(Random.Range(0, 2) == 1)
            xPosition = _GetDirectionVariation(targetPosition.x, playerColliderSize.y / 2);
        else
            yPosition = _GetDirectionVariation(targetPosition.y, playerColliderSize.x / 2);

        return new Vector2(xPosition, yPosition);
    }
}
