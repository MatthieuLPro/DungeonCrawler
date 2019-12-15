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
    [HideInInspector]
    public bool _isWaiting = false;
    [HideInInspector]
    public  bool _isWakeUpTransition    = false;
    [HideInInspector]
    public bool _isWakeDownTransition  = false;

    private bool _coWaitIsRunning = false;
    private bool _coMoveIsRunning = false;


    
    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    protected virtual void Start()
    {
        currentState    = EnemyState.idle;
        _parent         = transform.parent.gameObject;
        anime           = _parent.GetComponent<Animator>();
        rb2d            = _parent.GetComponent<Rigidbody2D>();

        if (_wakeSystem)
            _isWaiting = true;
    }

    public virtual void MainController()
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
    }

    /* ************************************************ */
    /* Movement functions */
    /* ************************************************ */

    // Linear movement (walk)
    private void ClassicMovement()
    {
        if(_waitTime == 0)
            MoveObject();
    }

    // Ooching movement (jump style)
    private void OochingMovement()
    {
        if(_waitTime == 0 || (!_isWaiting && _coMoveIsRunning))
            MoveObject();
        else if(_isWaiting && !_coWaitIsRunning)
            StartCoroutine(WaitCo());
        else if (!_isWaiting && !_coMoveIsRunning)
            StartCoroutine(MoveCo());
    }

    /* ************************************************ */
    /* WakeUp system functions */
    /* ************************************************ */

    public void WakeEnemy(){
        StartCoroutine(WakeUpCo());
    }
    
    public void SleepEnemy(){
        StartCoroutine(WakeDownCo());
    }

    private void RefreshDetection(){
        GetComponent<Transform>().parent.GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Transform>().parent.GetComponent<CircleCollider2D>().enabled = true;
    }

    /* ************************************************ */
    /* Animations */
    /* ************************************************ */

    private void AnimationIdle()
    {
        anime.SetBool("Moving", false);
        if (currentState != EnemyState.carry)
            currentState = EnemyState.idle;
    }

    private void AnimationMovement()
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
    private IEnumerator WaitCo()
    {
        _coWaitIsRunning = true;
            
        yield return new WaitForSeconds(_waitTime);

        _coWaitIsRunning = false;
        _isWaiting       = false;
    }

    private IEnumerator MoveCo()
    {
        _coMoveIsRunning = true;
            
        yield return new WaitForSeconds(_moveTime);

        _coMoveIsRunning = false;
        _isWaiting       = true;

        if (_wakeSystem)
        {
            AnimationIdle();
            SleepEnemy();
        }
    }

    /* For Wake up mode */
    private IEnumerator WakeUpCo()
    {
        GetComponent<Transform>().parent.GetComponent<CircleCollider2D>().enabled = false;
        anime.SetBool("WakeUp", true);
        _isWakeUpTransition = true;
        yield return new WaitForSeconds(_wakeTime);

        anime.SetBool("WakeUp", false);
        _isWakeUpTransition = false;
        _isWaiting          = false;
        GetComponent<Transform>().parent.GetChild(1).GetComponent<BoxCollider2D>().enabled  = true;
    }
    
    private IEnumerator WakeDownCo()
    {
        GetComponent<Transform>().parent.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        anime.SetBool("WakeDown", true);
        _isWakeDownTransition = true;
        yield return new WaitForSeconds(_wakeTime);

        anime.SetBool("WakeDown", false);
        _isWakeDownTransition = false;
        _isWaiting            = true;
        StartCoroutine(WaitWakeSystemCo());
    }

    private IEnumerator WaitWakeSystemCo()
    {            
        yield return new WaitForSeconds(_wakeTime);
        GetComponent<Transform>().parent.GetComponent<CircleCollider2D>().enabled = true;
    }

}
