using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourKnightPattern : MonoBehaviour
{
    [Header("Speed Settings")]
    public float speed = .0f;

    [Header("Phase Frequence Settings")]
    [SerializeField]
    private float _wakingTime       = .0f;
    [SerializeField]
    private float _phaseTime        = .0f;
    [SerializeField]
    private float _transitionTime   = .0f;

    [Header("Wake up Phase Settings")]
    [SerializeField]
    private float _shakeValue = 0.04f;

    [Header("Circle Phase Settings")]
    [SerializeField]
    private float _rotateSpeed = 5f;
    [SerializeField]
    private float _radius = 0.1f;
    [SerializeField]
    private float _angle = .0f;

    [Header("Transition Circle Phase Settings")]
    [SerializeField]
    private Vector3 _initCirclePos;

    [Header("Transition Line Phase Settings")]
    [SerializeField]
    private Vector3 _initLinePos;

    private GameObject      _parent;
    private Animator        _animator;
    private Rigidbody2D     _rb2d;

    private bool    _coWakingUp     = true;
    private bool    _coIsWorking    = false;
    private int     _phase          = 1;

    // Phase 1 => Circle
    // Phase 2 => Line
    
    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    protected virtual void Start()
    {
        _parent         = transform.parent.gameObject;
        _animator       = _parent.GetComponent<Animator>();
        _rb2d           = _parent.GetComponent<Rigidbody2D>();

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
        Vector3 shakePosition = new Vector3(_parent.transform.position.y + _shakeValue,
                                            _parent.transform.position.y,
                                            _parent.transform.position.z);

        _shakeValue *= -1;
        _parent.transform.position = shakePosition;
    }
    private void FirstPattern()
    {
        if (_phase == 1)
        {
            // Transition To Phase 1
            if (!_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(2, _transitionTime));
            else
                TransitionPhaseCircleMovement();
        }
        else if (_phase == 2)
        {
            // Phase 1
            if (!_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(3, _phaseTime));
            else
                CirclePhaseMovement();
        }
        else if (_phase == 3)
        {
            // Transition to Phase 2
            /*if (!_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(4, _transitionTime));
            else*/
            TransitionPhaseLineMovement(_initLinePos);
        }
        else if (_phase == 4)
        {
            // Phase 2
            if (!_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(1, _phaseTime));
            else
                LinePhaseMovement();
        }
    }

    private void SecondPattern()
    {
        Debug.Log("Second Pattern");
    }

    /* ************************************************ */
    /* Phase functions */
    /* ************************************************ */
    // Transition to Circle phase
    private void TransitionPhaseCircleMovement()
    {
        Debug.Log("Transition to Circle Line");
    }

    // Circle movement
    private void CirclePhaseMovement()
    {
        _angle += _rotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(_angle) * _radius, Mathf.Cos(_angle) * _radius, _parent.transform.position.z);
        _parent.transform.position = _parent.transform.position + offset;
    }

    // Transition to Line phase
    private void TransitionPhaseLineMovement(Vector3 targetPos)
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
            (_parent.transform.position.y < targetPos.y + 0.1f) && (_parent.transform.position.y > targetPos.y - 0.1f))
        {
            _phase = 4;
        }

    }

    // Line movement
    private void LinePhaseMovement()
    {
        _rb2d.MovePosition(_parent.transform.position + new Vector3(0, -1.0f, 0) * speed * Time.deltaTime);
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
    }


    // Transition to Phase and phase wait CoRoutine
    private IEnumerator TransitionAndPhaseCo(int nextPhase, float waitTime)
    {
        _coIsWorking = true;
        yield return new WaitForSeconds(waitTime);
        
        _phase          = nextPhase;
        _coIsWorking    = false;
    }
}
