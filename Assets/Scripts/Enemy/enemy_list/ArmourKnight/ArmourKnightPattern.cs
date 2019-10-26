using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourKnightPattern : MonoBehaviour
{
    [Header("Speed Settings")]
    public float   speed = .0f;

    [Header("Phase Frequence Settings")]
    [SerializeField]
    private float _wakingTime = .0f;
    [SerializeField]
    private float _pattern1Phase1Time = .0f;
    [SerializeField]
    private float _pattern1Phase2Time   = .0f;
        
    private GameObject      _parent;
    private Animator        _animator;
    private Rigidbody2D     _rb2d;

    private bool    _coWakingUp     = true;
    private bool    _coIsWorking    = false;
    private int     _phase          = 0;

    // Phase 0 => Circle
    // Phase 1 => Line
    
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
            if (_animator.GetInteger("Pattern") == 0)
                FirstPattern();
            else
                SecondPattern();
        }
        else
            Debug.Log("Idle Time");
    }

    private void WakeUpPattern()
    {
        Debug.Log("Wake Up Pattern");
    }

    private void FirstPattern()
    {
        if (_phase == 0 && !_coIsWorking)
        {
            Debug.Log("Start Phase 1 time");
            StartCoroutine(Phase1Co());
        }
        else if (_phase == 1 && !_coIsWorking)
        {
            Debug.Log("Start Phase 2 time");
            StartCoroutine(Phase2Co());
        }

        if (_phase == 0)
            Debug.Log("Behaviour phase 1");
        else if (_phase == 1)
            Debug.Log("Behaviour phase 2");
    }

    private void SecondPattern()
    {
        Debug.Log("Second Pattern");
    }

    /*public virtual void MainController()
    {
        if (_wakeSystem)
        {
            if (_isWakeUpTransition || _isWakeDownTransition || _isWaiting)
            {
                return;
            }
            else if(_moveTime > 0 && !_coMoveIsRunning)
                StartCoroutine(MoveCo());
            else 
                ClassicMovement();
        }
        else if (changePos != Vector3.zero && currentState != EnemyState.attack)
        {
            if (_waitTime > 0)
                OochingMovement();
            else
                ClassicMovement();
        }
        else
            AnimationIdle();
    }
    
    private void MoveObject()
    {
        changePos.Normalize();
        rb2d.MovePosition(transform.position + changePos * speed * Time.deltaTime);
        AnimationMovement();
    }*/

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

    // Circle Pattern
    private IEnumerator Phase1Co()
    {
        _coIsWorking = true;
        yield return new WaitForSeconds(_pattern1Phase1Time);
        
        _coIsWorking = false;
    }

    // Line Pattern
    private IEnumerator Phase2Co()
    {
        _coIsWorking = true;
        yield return new WaitForSeconds(_pattern1Phase2Time);
        
        _coIsWorking = false;
    }
}
