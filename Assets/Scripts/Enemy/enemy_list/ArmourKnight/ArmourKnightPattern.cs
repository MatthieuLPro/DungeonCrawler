﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourKnightPattern : MonoBehaviour
{
    [Header("Speed Settings")]
    public float speed = 2f;

    [Header("Phase Frequence Settings")]
    [SerializeField]
    private float _wakingTime       = 1.5f;

    [Header("Wake up Phase Settings")]
    [SerializeField]
    private float _shakeValue = 0.04f;

    [Header("Circle Phase Settings")]
    [SerializeField]
    private float _rotateSpeed = 2f;
    [SerializeField]
    private float _radius = 0.1f;
    [SerializeField]
    private Vector3 _center;

    [Header("Transition Circle Phase Settings")]
    [SerializeField]
    private Vector3 _initCirclePos = Vector3.zero;

    [Header("Transition Line Phase Settings")]
    [SerializeField]
    private Vector3 _initLinePos = Vector3.zero;

    private GameObject      _system;
    private GameObject      _parent;
    private Animator        _animator;
    private Rigidbody2D     _rb2d;

    private float _angle = .0f;
    private bool _coWakingUp;
    
    [HideInInspector]
    public bool ready;

    // Phase 1 => Circle
    // Phase 2 => Line
    
    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    protected virtual void Start()
    {
        _system         = transform.parent.transform.parent.gameObject;
        _parent         = transform.parent.gameObject;
        _animator       = _parent.GetComponent<Animator>();
        _rb2d           = _parent.GetComponent<Rigidbody2D>();
        ready           = false;
        _coWakingUp     = true;

        _angle = Mathf.Acos((_initCirclePos.x - _center.x) / _radius);

        Debug.Log("(DEBUT) Angle of : " + _parent.gameObject.name + "\n" +
                  "_initCirclePos.x - _center.x = " + (_initCirclePos.x - _center.x) + "\n" + 
                  "(_initCirclePos.x - _center.x) / _radius = " + ((_initCirclePos.x - _center.x) / _radius) + "\n" +
                  "_angle(radian) = " + _angle + "\n" +
                  "_angle(degree) = " + (_angle * 180 / Mathf.PI));

        StartCoroutine(WakingUpCo());
    }

    private void FixedUpdate()
    {
        if (_coWakingUp)
        {
            WakeUpPattern();
            return;
        }

        if (_animator.GetBool("Moving"))
        {
            if (_animator.GetInteger("Pattern") == 1)
                FirstPattern();
            else if (_animator.GetInteger("Pattern") == 2)
                SecondPattern();
        }
        else
            Debug.Log("Idle Time");
    }
    
    /* ************************************************ */
    /* Pattern functions */
    /* ************************************************ */
    private void WakeUpPattern()
    {
        Vector3 shakePosition = new Vector3(_parent.transform.position.x + _shakeValue,
                                            _parent.transform.position.y,
                                            _parent.transform.position.z);

        _shakeValue *= -1;
        _parent.transform.position = shakePosition;
    }

    private void FirstPattern()
    {
        if (_system.GetComponent<ArmourKnightSystem>().phase == 1)
            TransitionPhaseMovement(_initCirclePos);
        else if (_system.GetComponent<ArmourKnightSystem>().phase == 2)
            CirclePhaseMovement();
        else if (_system.GetComponent<ArmourKnightSystem>().phase == 3)
            TransitionPhaseMovement(_initLinePos);
        else if (_system.GetComponent<ArmourKnightSystem>().phase == 4)
            LinePhaseMovement();
    }

    private void SecondPattern()
    {
        Debug.Log("Second Pattern");
    }

    /* ************************************************ */
    /* Phase functions */
    /* ************************************************ */
    // Circle movement
    private void CirclePhaseMovement()
    {
        _angle += _rotateSpeed * Time.deltaTime;

        Debug.Log("(AFTER) Angle of : " + _parent.gameObject.name + "\n" +
                  "_angle = " + _angle + "\n" +
                  "_angle(degree) = " + (_angle * 180 / Mathf.PI));


        var offset = new Vector3(Mathf.Sin(_angle) * _radius, Mathf.Cos(_angle) * _radius, _parent.transform.position.z);

        _parent.transform.position = _center + offset;
    }

    // Line movement
    private void LinePhaseMovement(){
        _rb2d.MovePosition(_parent.transform.position + new Vector3(0, -1.0f, 0) * speed * Time.deltaTime);
    }

    // Transition phase
    private void TransitionPhaseMovement(Vector3 targetPos)
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        if (_parent.transform.position.x > targetPos.x)
            newPosition.x = -1.0f;
        else if (_parent.transform.position.x < targetPos.x)
            newPosition.x = 1.0f;
        else
            newPosition.x = 0.0f;

         if (_parent.transform.position.y > targetPos.y)
            newPosition.y = -1.0f;
        else if (_parent.transform.position.y < targetPos.y)
            newPosition.y = 1.0f;
        else
            newPosition.y = 0.0f;

        _rb2d.MovePosition(_parent.transform.position + newPosition * speed * Time.deltaTime);

        if ((_parent.transform.position.x < targetPos.x + 0.1f) && (_parent.transform.position.x > targetPos.x - 0.1f) &&
            (_parent.transform.position.y < targetPos.y + 0.1f) && (_parent.transform.position.y > targetPos.y - 0.1f) &&
             !ready)
        {
            ready = true;
        }
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    // Waking up before battle
    private IEnumerator WakingUpCo()
    {
        yield return new WaitForSeconds(_wakingTime);
        
        _animator.SetInteger("Pattern", 1);
        _animator.SetBool("Moving", true);
        _coWakingUp = false;
        _system.GetComponent<ArmourKnightSystem>().phase = 1;
    }
}
