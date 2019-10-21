using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    knockBack,
    carry
}

public abstract class EnemyMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float   speed;

    [Header("Movement Frequence Settings")]
    [SerializeField]
    private float _waitTime = .0f;
    [SerializeField]
    private float _moveTime = .0f;

    [Header("Wake up down Settings")]
    [SerializeField]
    private bool _wakeSystem = false;
    [SerializeField]
    private float _wakeTime = .0f;
    
    private GameObject _parent;

    [HideInInspector]
    public Vector3 changePos;

    [HideInInspector]
    public EnemyState currentState;

    [HideInInspector]
    public Animator anime;

    [HideInInspector]
    public Rigidbody2D rb2d;

    private bool _coWaitIsRunning = false;
    private bool _coMoveIsRunning = false;

    public  bool _isWakeUpTransition    = false;
    private bool _isWakeDownTransition  = false;
    private bool _isWaiting             = false;
    
    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    protected virtual void Start(){
        currentState    = EnemyState.idle;
        _parent         = transform.parent.gameObject;
        anime           = _parent.GetComponent<Animator>();
        rb2d            = _parent.GetComponent<Rigidbody2D>();
    }

    public virtual void MainController(){
        if (changePos != Vector3.zero && currentState != EnemyState.attack)
        {
            Debug.Log("Condition move: " + (_waitTime == 0 || (!_isWaiting && _coMoveIsRunning && (!_isWakeUpTransition && !_isWakeDownTransition))));
            if(_waitTime == 0 || (!_isWaiting && _coMoveIsRunning && (!_isWakeUpTransition && !_isWakeDownTransition)))
                MoveObject();
            else if (_isWakeUpTransition)
                StartCoroutine(WakeUpCo());
            else if (_isWakeDownTransition)
                StartCoroutine(WakeDownCo());
            else if(_isWaiting && !_coWaitIsRunning)
                StartCoroutine(WaitCo());
            else if (!_isWaiting && !_coMoveIsRunning)
                StartCoroutine(MoveCo());
        }
        else
            AnimationIdle();
    }
    
    protected void MoveObject(){
        changePos.Normalize();
        rb2d.MovePosition(transform.position + changePos * speed * Time.deltaTime);
        AnimationMovement();
    }

    /* ************************************************ */
    /* Animations */
    /* ************************************************ */

    protected void AnimationIdle(){
        anime.SetBool("Moving", false);
        if (currentState != EnemyState.carry)
            currentState = EnemyState.idle;
    }

    protected void AnimationMovement()
    {
        anime.SetFloat("DirectionX", changePos.x);
        anime.SetFloat("DirectionY", changePos.y);
        anime.SetBool("Moving", true);
        if (currentState == EnemyState.carry)
            return;

        currentState = EnemyState.walk;
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    /* For Wait and Move mode */
    private IEnumerator WaitCo(){
        _coWaitIsRunning = true;
            
        yield return new WaitForSeconds(_waitTime);

        _coWaitIsRunning = false;
        _isWaiting       = false;

        if (_wakeSystem){
            GetComponent<Transform>().parent.GetComponent<BoxCollider2D>().enabled    = true;
            GetComponent<Transform>().parent.GetComponent<CircleCollider2D>().enabled = false;
        }

    }

    private IEnumerator MoveCo(){
        _coMoveIsRunning = true;
            
        yield return new WaitForSeconds(_moveTime);

        _coMoveIsRunning = false;
        _isWaiting       = true;

        if (_wakeSystem){
            GetComponent<Transform>().parent.GetComponent<BoxCollider2D>().enabled    = false;
            GetComponent<Transform>().parent.GetComponent<CircleCollider2D>().enabled = true;
            _isWakeDownTransition = true;
        }
    }

    /* For Wake up mode */
    private IEnumerator WakeUpCo(){
        anime.SetBool("WakeUp", true);
        _isWakeUpTransition = true;
        yield return new WaitForSeconds(_wakeTime);

        anime.SetBool("WakeUp", false);
        _isWakeUpTransition = false;
    }
    
    private IEnumerator WakeDownCo(){
        anime.SetBool("WakeDown", true);
        _isWakeDownTransition = true;
        yield return new WaitForSeconds(_wakeTime);

        anime.SetBool("WakeDown", false);
        _isWakeDownTransition = false;
    }    
}
