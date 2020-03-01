using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Always the same direction (x; y)
public class DeterminateSingleDirections : DeterminateDirections
{
    [Header("Direction Settings")]
    [SerializeField]
    private float _directionX = 0;
    [SerializeField]
    private float _directionY = 0;
    
    public float DirectionX {
        get { return _directionX; }
        set { _directionX = value; }
    }
    public float DirectionY {
        get { return _directionY; }
        set { _directionY = value; }
    }

    override public Vector2 GetDirection(){
        return new Vector2(DirectionX, DirectionY);
    }
}
