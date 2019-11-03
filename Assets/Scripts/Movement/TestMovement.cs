using System.Collections;
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
    public bool    hasManyForce;

    [HideInInspector]
    public float maxSpeedTemp;

    [HideInInspector]
    public Vector3     newDirection;
    public Vector3     oldDirection;

    [HideInInspector]
    public TestObjectState currentState;

    [HideInInspector]
    public Animator anime;

    [HideInInspector]
    public Rigidbody2D   _rb2d;

    public bool isWalking;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Start()
    {
        currentState = TestObjectState.idle;
        anime = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        maxSpeedTemp = maxSpeed;
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
        oldDirection = newDirection;
        newDirection = Vector3.zero;
        newDirection.x = inputMainPosition.x;
        newDirection.y = inputMainPosition.y;
        newDirection.Normalize();
        isWalking = (newDirection.x != 0 || newDirection.y != 0);
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
        if(_rb2d.velocity.magnitude > maxSpeedTemp)
            _rb2d.velocity = _rb2d.velocity.normalized * maxSpeedTemp;
        else
        {
            if (newDirection.x == -oldDirection.x)
                _rb2d.AddForce(new Vector2(newDirection.x * (acceleration * 2.0f), newDirection.y * acceleration), ForceMode2D.Impulse);
            else if (newDirection.y == -oldDirection.y)
                _rb2d.AddForce(new Vector2(newDirection.x * acceleration, newDirection.y * (acceleration * 2.0f)), ForceMode2D.Impulse);
            else
                _rb2d.AddForce(newDirection * acceleration, ForceMode2D.Impulse);
        }
    }

    /* Movement decceleration */
    private void Decceleration()
    {
        if (_rb2d.velocity == Vector2.zero)
            return;

        if ( _rb2d.velocity.x >= -decceleration && _rb2d.velocity.x <= decceleration && _rb2d.velocity.y >= -decceleration && _rb2d.velocity.y <= decceleration)
            _rb2d.velocity = Vector2.zero;

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
