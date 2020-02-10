using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeterminateSingleDirections : DeterminateDirections
{
    [Header("Direction Settings")]
    [SerializeField]
    private int _directionX = 0;
    [SerializeField]
    private int _directionY = 0;
    
    public int _DirectionX { get; set; }
    public int _DirectionY { get; set; }

    void Start()
    {
        if (_DirectionX != -1 && _DirectionX != 1)
            _DirectionX = 0;

        if (_DirectionY != -1 && _DirectionY != 1)
            _DirectionY = 0;
    }

    override protected Vector2 _GetDirection(){
        return new Vector2(_DirectionX, _DirectionY);
    }
}
