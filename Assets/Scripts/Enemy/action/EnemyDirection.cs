using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Variables */
/* _movementSequence : Necessary time to complete a full deplacement */
/* _movementAngle : Angle of the movement, usefull only if the deplacement type is only diagonal */

public class EnemyDirection : EnemyMovement
{
    [Header("Direction Type Options")]
    [SerializeField]
    private bool _enableDiagonal = false;
    public bool  onlyTop         = false;
    public bool  onlyRight       = false;
    public bool  onlyDown        = false;
    public bool  onlyLeft        = false;

    [Header("Movement Settings")]
    [SerializeField]
    private int _movementSequence   = 50;
    [SerializeField]
    private float _movementAngle    = 0.25f;
    [SerializeField]
    private float _chaseRadius      = 0.0f;
    [SerializeField]
    private float _chaseLength      = 0.0f;

    [Header("Direction Options")]
    [SerializeField]
    private bool _enableHunt              = false;
    [SerializeField]
    private bool _randomDirection         = false;
    [SerializeField]
    private bool _onlyDiagonalDirection   = false;

    private int _movementTimer = 0;
    private int _movementLimit;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    private void Awake()
    {
        _movementLimit = _movementSequence;
        if (_onlyDiagonalDirection)
            _SetNewRandomDiagonalDirection();
    }

    private void FixedUpdate()
    {
        if (_enableHunt || _onlyDiagonalDirection)
            AIdirection();
        else if (_randomDirection)
            RandomDirection();
        else if (onlyTop)
            changePos.y = 1;
        else if (onlyRight)
            changePos.x = 1;
        else if (onlyDown)
            changePos.y = -1;
        else if (onlyLeft)
            changePos.x = -1;
        else
        {
            changePos.x = 0;
            changePos.y = 0;
        }
        MainController();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_onlyDiagonalDirection)
        {
            _SetNewRandomDiagonalDirection();
            StartCoroutine(RefreshBoxCo());
        }
    }

    /* ************************************************ */
    /* Find directions function */
    /* ************************************************ */
    // Object has a behaviour
    private void AIdirection()
    {
        if (!_enableDiagonal)
        {
            if (!MovementIsComplete())
                return;

            if (_movementSequence <= 5)
                _movementSequence = _movementLimit;
        }

        if (_enableHunt)
            HuntDirection();
        else if (_onlyDiagonalDirection)
            return;
    }

    /* ************************************************ */
    /* New directions for direction type HUNTER */
    /* ************************************************ */
    /* If _enableHunt == true */
    // New direction depending of target position
    private void HuntDirection()
    {
        GameObject player               = GameObject.FindWithTag("Player");
        Vector2 targetPosition          = player.transform.position;
        Vector2 playerColliderSize      = player.GetComponent<BoxCollider2D>().size;

        if (_TargetIsTooClose(targetPosition))
        {
            _SetStopHunting();
            return;
        }

        _SetHunterDirections(targetPosition, playerColliderSize);
    }

    private bool _TargetIsTooClose(Vector2 targetPosition)
    {
        if (_chaseRadius <= 0 || _chaseLength <= 0)
            return false;

        if (Vector3.Distance(targetPosition, transform.position) < _chaseRadius ||
            Vector3.Distance(targetPosition, transform.position) > _chaseLength || _chaseLength == 0)
            return true;

        return false;
    }

    private void _SetStopHunting()
    {
        changePos.x = 0;
        changePos.y = 0;
    }

    private void _SetHunterDirections(Vector2 targetPosition, Vector2 playerColliderSize)
    {
        if(_enableDiagonal)
        {
            _SetHuntHorizontalDirection(targetPosition, playerColliderSize);
            _SetHuntVerticalDirection(targetPosition, playerColliderSize);
        }
        else
        {
            if(Random.Range(0, 2) == 1)
                _SetHuntHorizontalDirection(targetPosition, playerColliderSize);
            else
                _SetHuntVerticalDirection(targetPosition, playerColliderSize);
        }
    }

    private void _SetHuntHorizontalDirection(Vector2 targetPosition, Vector2 playerColliderSize)
    {
        if (targetPosition.x > targetPosition.x + playerColliderSize.y / 2)
            changePos.x = -1.0f;
        else if (targetPosition.x < targetPosition.x - playerColliderSize.y / 2)
            changePos.x = 1.0f;
        else
            changePos.x = 0.0f;
    }

    private void _SetHuntVerticalDirection(Vector2 targetPosition, Vector2 playerColliderSize)
    {
        if (targetPosition.y > targetPosition.y + playerColliderSize.x / 2)
            changePos.y = -1.0f;
        else if (targetPosition.y < targetPosition.y - playerColliderSize.x / 2)
            changePos.y = 1.0f;
        else
            changePos.y = 0.0f;
    }

    /* ************************************************ */
    /* Random directions */
    /* ************************************************ */
    /* If _enableHunt == false */
    // New direction depending of target position
    // Set a new random direction on X && Y
    private void _SetNewRandomDirection(){
        changePos = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    // Set a new random direction on X || Y
    private void _SetNewRandomUniqueDirection(){
        if (Random.Range(0, 2) == 0)
            changePos.x = Random.Range(-1.0f, 1.0f);
        else
            changePos.y = Random.Range(-1.0f, 1.0f);
    }
    
    // Set a new random diagonal direction
    private void _SetNewRandomDiagonalDirection()
    {
        _SetRandomXDirection();
        _SetRandomYDirection();
    }

    // Set a new random angle for X
    private void _SetRandomXDirection()
    {
        if (changePos.x == 0)
        {
            if (Random.Range(0, 2) == 1)
                changePos.x = _movementAngle;
            else
                changePos.x = -1 * _movementAngle;
        }
        else
            changePos.x = -1 * changePos.x;
    }

    // Set a new random angle for Y
    private void _SetRandomYDirection()
    {
        int angle = (Random.Range(0, 2) == 1 ? 1 : -1);

        changePos.y = angle * _movementAngle;
    }

    // New direction depending of player _randomDirection (X OR Y OR X AND Y)
    private void RandomDirection()
    {
        if (!MovementIsComplete())
            return;

        if (_movementSequence <= 5)
            _movementSequence = _movementLimit;

        if (_enableDiagonal)
            _SetNewRandomDirection();
        else
            _SetNewRandomUniqueDirection();
    }

    /* ************************************************ */
    /* Movement Verification */
    /* ************************************************ */
    // If the movement is totally complete or not
    // Should change this by a CoRoutine
    private bool MovementIsComplete()
    {
        if (_movementSequence == 0)
            return false;

        if (_movementTimer <= _movementSequence)
        {
            _movementTimer++;
            return false;
        }

        _movementTimer = 0;
        changePos = Vector3.zero;
        return true;
    }
    
    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    // Refresh the box colliders
    private IEnumerator RefreshBoxCo(){
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);

        GetComponent<BoxCollider2D>().enabled = true;
    }

    /* ************************************************ */
    /* Setter */
    /* ************************************************ */
    public void UpdateDirection(string direction)
    {
        onlyTop     = false;
        onlyRight   = false;
        onlyDown    = false;
        onlyLeft    = false;

        switch(direction)
        {
            case "top":
                onlyTop = true;
                break;
            case "right":
                onlyRight = true;
                break;
            case "down":
                onlyDown = true;
                break;
            default:
                onlyLeft = true;
                break;
        }
    }
}
