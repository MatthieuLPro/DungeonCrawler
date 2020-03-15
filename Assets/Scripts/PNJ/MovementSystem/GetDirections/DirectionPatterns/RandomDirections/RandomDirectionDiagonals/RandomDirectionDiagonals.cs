using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionDiagonals : RandomDirections
{
    [Header("Diagonal Direction Settings")]
    [SerializeField]
    private float _movementAngle = 0.25f;

    override protected Vector2 _GetDirection(){
        return _GetDiagonalVariation();
    }

    private Vector2 _GetDiagonalVariation(){
        return new Vector2(_GetDiagonalXAngleVariation(), _GetDiagonalYAngleVariation());
    }

    // Get angle variation on X
    private float _GetDiagonalXAngleVariation()
    {
        int angle = (Random.Range(0, 2) == 1 ? 1 : -1);

        if (PosVariation.x == 0)
            return angle * _movementAngle;

        return -1 * PosVariation.x;
    }

    // Get angle variation on Y
    private float _GetDiagonalYAngleVariation()
    {
        int angle = (Random.Range(0, 2) == 1 ? 1 : -1);

        return angle * _movementAngle;
    }
}
