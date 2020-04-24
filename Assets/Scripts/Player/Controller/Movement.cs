using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TestObjectState {
    idle,
    walk,
    knock,
    attack,
    special
}

public class Movement : MonoBehaviour
{
    [Header("General Movement parameters")]
    public float   acceleration;
    public float   decceleration;
    public float   maxSpeed;

    /* Temp values */
    [HideInInspector]
    public float accelerationTemp;
    [HideInInspector]
    public float deccelerationTemp;
    [HideInInspector]
    public float maxSpeedTemp;

    /* Directions */
    [HideInInspector]
    public  Vector3    newDirection;
    private Vector3    _oldDirection;

    /* State machine */
    [HideInInspector]
    public TestObjectState currentState;

    /* Object components */
    private GameObject  _parent;
    private Animator    _anime;
    private Rigidbody2D _rb2d;

    /* Other force than movement on the object */
    [HideInInspector]
    public bool hasManyForce;
    [HideInInspector]
    public Vector3 otherForce;

    /* Type of floor */
    [HideInInspector]
    public bool iceFloor;

    /* Other */
    private bool _movementIsBlock;

    private bool _isDashing;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Start()
    {
        currentState = TestObjectState.idle;

        _parent = transform.parent.gameObject;
        _anime  = _parent.GetComponent<Animator>();
        _rb2d   = _parent.GetComponent<Rigidbody2D>();

        MaxSpeedTemp     = MaxSpeed;
        accelerationTemp = acceleration;

        /* Decceleration in [0,1] */
        if (decceleration > 1)
            decceleration = 1;
        if (decceleration < 0)
            decceleration = 0;

        deccelerationTemp = decceleration;
        IsDashing = false;
    }

    void FixedUpdate()
    {
        if (MovementIsBlock)
            return;
        PlayerMovement();
    }

    /* ************************************************ */
    /* Movement functions */
    /* ************************************************ */
    public void SetPlayerDirection(Vector2 inputDirection) 
    {
        Vector3 inputCurrentPosition = Vector3.zero;

        inputDirection.x        = (float)Math.Round(inputDirection.x);
        inputDirection.y        = (float)Math.Round(inputDirection.y);
        inputCurrentPosition    = new Vector3(inputDirection.x, inputDirection.y, 0);
        _oldDirection           = newDirection;
        newDirection            = inputCurrentPosition;
    }

    /* Move or idle depend of vector newDirection */
    private void PlayerMovement()
    {
        if (newDirection != Vector3.zero)
        {
            Acceleration();
            AnimationMovement();
        }
        else if (hasManyForce)
        {
            Acceleration();
            AnimationIdle();
        }
        else
        {
            Decceleration();
            AnimationIdle();
        }
    }

    /* Movement acceleration */
    private void Acceleration()
    {
        if (!hasManyForce && !iceFloor)
        {
            if (((newDirection.x == -_oldDirection.x) && newDirection.x != 0) || ((newDirection.y == -_oldDirection.y) && newDirection.y != 0))
                _rb2d.velocity = Vector2.zero;
        }

        if(_rb2d.velocity.magnitude > MaxSpeedTemp)
            _rb2d.velocity = _rb2d.velocity.normalized * MaxSpeedTemp;
        else
            _rb2d.AddForce(newDirection * accelerationTemp + otherForce, ForceMode2D.Impulse);
    }

    /* Movement decceleration */
    private void Decceleration()
    {
        if (_rb2d.velocity == Vector2.zero)
            return;

        if (!iceFloor)
        {
            if ( _rb2d.velocity.x >= -deccelerationTemp && _rb2d.velocity.x <= deccelerationTemp && _rb2d.velocity.y >= -deccelerationTemp && _rb2d.velocity.y <= deccelerationTemp)
                _rb2d.velocity = Vector2.zero;
        }
        _rb2d.velocity = _rb2d.velocity * deccelerationTemp;
    }
    
    /* ************************************************ */
    /* Animations */
    /* ************************************************ */
    /* Idle */
    private void AnimationIdle(){
        _anime.SetBool("Moving", false);
        currentState = TestObjectState.idle;
    }

    /* Movement */
    private void AnimationMovement()
    {
        _anime.SetFloat("DirectionX", newDirection.x);
        _anime.SetFloat("DirectionY", newDirection.y);
        _anime.SetBool("Moving", true);
        currentState = TestObjectState.walk;
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    public float MaxSpeed {
        get => maxSpeed;
        set => maxSpeed = value;
    }

    public float MaxSpeedTemp {
        get => maxSpeedTemp;
        set => maxSpeedTemp = value;
    }

    public bool MovementIsBlock {
        get => _movementIsBlock;
        set => _movementIsBlock = value;
    }

    public bool IsDashing {
        get => _isDashing;
        set => _isDashing = value;
    }

    public Rigidbody2D Rb2d {
        get => _rb2d;
        set => _rb2d = value;
    }
}
