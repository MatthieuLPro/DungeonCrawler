﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TestObjectState{
    idle,
    walk
}

public class TestMovement : MonoBehaviour
{
    [Header("General Movement parameters")]
    public float   acceleration;
    public float   decceleration;
    public float   maxSpeed;

    /* Temp values */
    [HideInInspector]
    public float accelerationTemp;
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
    [HideInInspector]
    public Animator anime;
    [HideInInspector]
    public Rigidbody2D   _rb2d;

    /* If other force than movement is on the object */
    [HideInInspector]
    public bool hasManyForce;

    /* Type of floor */
    [HideInInspector]
    public bool iceFloor;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Start()
    {
        currentState = TestObjectState.idle;
        anime = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        maxSpeedTemp = maxSpeed;
        accelerationTemp = acceleration;
    }

    void FixedUpdate()
    {
        PlayerDirection();
        PlayerMovement();
    }

    /* ************************************************ */
    /* Movement functions */
    /* ************************************************ */
    /* Get Direction */
    private void PlayerDirection()
    {
        Vector3 inputMainPosition = InputManager.MainJoystick();

        _oldDirection = newDirection;
        newDirection = inputMainPosition;
        newDirection.Normalize();
    }

    /* Move or idle depend of vector newDirection */
    private void PlayerMovement()
    {
        if (newDirection != Vector3.zero)
        {
            Acceleration();
            AnimationMovement();
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

        if(_rb2d.velocity.magnitude > maxSpeedTemp)
            _rb2d.velocity = _rb2d.velocity.normalized * maxSpeedTemp;
        else
            _rb2d.AddForce(newDirection * accelerationTemp, ForceMode2D.Impulse);
    }

    /* Movement decceleration */
    private void Decceleration()
    {
        if (_rb2d.velocity == Vector2.zero)
            return;

        if (!iceFloor)
        {
            if ( _rb2d.velocity.x >= -decceleration && _rb2d.velocity.x <= decceleration && _rb2d.velocity.y >= -decceleration && _rb2d.velocity.y <= decceleration)
                _rb2d.velocity = Vector2.zero;
        }

        _rb2d.velocity = _rb2d.velocity.normalized * decceleration;
    }
    
    /* ************************************************ */
    /* Animations */
    /* ************************************************ */
    /* Idle */
    private void AnimationIdle(){
        anime.SetBool("Moving", false);
        currentState = TestObjectState.idle;
    }

    /* Movement */
    private void AnimationMovement()
    {
        anime.SetFloat("DirectionX", newDirection.x);
        anime.SetFloat("DirectionY", newDirection.y);
        anime.SetBool("Moving", true);
        currentState = TestObjectState.walk;
    }
}