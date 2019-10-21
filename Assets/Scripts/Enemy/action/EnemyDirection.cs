using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : EnemyMovement
{
    [Header("Direction Type Settings")]
    [SerializeField]
    private bool _enableDiagonal    = false;
    [SerializeField]
    private bool _onlyTop           = false;
    [SerializeField]
    private bool _onlyRight         = false;
    [SerializeField]
    private bool _onlyDown          = false;
    [SerializeField]
    private bool _onlyLeft          = false;

    [Header("Movement Settings")]
    [SerializeField]
    private int _movementSequence   = 50;
    [SerializeField]
    private float _movementAngle    = 0.25f;
    [SerializeField]
    private float _chaseRadius      = 0.0f;
    [SerializeField]
    private float _chaseLength      = 0.0f;

    [Header("Direction behaviour Settings")]
    [SerializeField]
    private bool _enableHunt                = false;
    [SerializeField]
    private bool _randomDirection           = false;
    [SerializeField]
    private bool _randomDiagonalDirection   = false;

    private int _movementTimer = 0;
    private int _movementLimit;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    private void Awake(){
        _movementLimit = _movementSequence;
        if (_randomDiagonalDirection)
            RandomDiagonal();
    }

    private void FixedUpdate(){
        if (_enableHunt || _randomDiagonalDirection)
            AIdirection();
        else if (_randomDirection)
            RandomDirection();
        else if (_onlyTop)
            changePos.y = 1;
        else if (_onlyRight)
            changePos.x = 1;
        else if (_onlyDown)
            changePos.y = -1;
        else if (_onlyLeft)
            changePos.x = -1;
        MainController();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (_randomDiagonalDirection){
            RandomDiagonal();
            StartCoroutine(RefreshBoxCo());
        }
    }

    /* ************************************************ */
    /* Find directions function */
    /* ************************************************ */
    // Object has a behaviour
    private void AIdirection(){
        if (!CheckSequence())
            return;

        if (_enableHunt){
            if (_movementSequence > 5) _movementSequence = 2;
            HuntDirection();
        }
        else if (_randomDiagonalDirection)
            return;
    }

    /* ************************************************ */
    /* Find directions function */
    /* ************************************************ */
    // New direction depending of player _enableHunt
    private void HuntDirection(){
        GameObject player   = GameObject.FindWithTag("Player");
        Transform target    = player.transform;
        Vector2 size        = player.GetComponent<BoxCollider2D>().size;

        if (Vector3.Distance(target.position, transform.position) < _chaseRadius ||
            Vector3.Distance(target.position, transform.position) > _chaseLength || _chaseLength == 0){
            changePos.x = 0;
            changePos.y = 0;
            return;
        }

        if (transform.position.x > target.position.x + size.y / 2)
            changePos.x = -1.0f;
        else if (transform.position.x < target.position.x - size.y / 2)
            changePos.x = 1.0f;
        else
            changePos.x = 0.0f;

        if (transform.position.y > target.position.y + size.x / 2)
            changePos.y = -1.0f;
        else if (transform.position.y < target.position.y - size.x / 2)
            changePos.y = 1.0f;
        else
            changePos.y = 0.0f;
    }

    // New direction depending of player _randomDirection (X OR Y OR X AND Y)
    private void RandomDirection(){
        if (!CheckSequence())
            return;

        if (_movementSequence <= 5)
            _movementSequence = _movementLimit;

        if (_enableDiagonal)
            RandomAllDirections();
        else
            RandomOneDirection();
    }

    // New direction if _randomDiagonalDirection (Only diagonal movement)
    public void RandomDiagonal(){
        if (changePos.x == 0){
            if (GetRandomBool())
                changePos.x = _movementAngle;
            else
                changePos.x = -1 * _movementAngle;
        }
        else
            changePos.x = -1 * changePos.x;
        
        if (GetRandomBool())
            changePos.y = _movementAngle;
        else
            changePos.y = -1 * _movementAngle;
    }

    /* ************************************************ */
    /* Enemy Direction functions */
    /* ************************************************ */
    // Random direction on X AND Y
    private void RandomAllDirections(){
        changePos.x = Random.Range(-1.0f, 1.0f);
        changePos.y = Random.Range(-1.0f, 1.0f);
    }
    // Random direction on X OR Y
    private void RandomOneDirection(){
        float direction = Random.Range(-1.0f, 1.1f);

        if (direction > 0)
            changePos.x = Random.Range(-1.0f, 1.0f);
        else
            changePos.y = Random.Range(-1.0f, 1.0f);
    }

    // If the movement sequence is done
    private bool CheckSequence(){
        if (_movementSequence == 0)
            return false;

        if (_movementTimer <= _movementSequence){
            _movementTimer++;
            return false;
        }

        _movementTimer = 0;
        changePos = Vector3.zero;
        return true;
    }

    /* ************************************************ */
    /* General functions */
    /* ************************************************ */
    // Random value true or false
    private bool GetRandomBool(){
        if (Random.Range(0, 2) == 1)
            return (true);

        return (false);
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
}
