using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirection : EnemyMovement
{
    [Header("Movement Type Settings")]
    [SerializeField]
    private bool _enableDiagonal = true;

    [Header("Movement Settings")]
    [SerializeField]
    private int _movementSequence = 50;
    [SerializeField]
    private float _chaseRadius = 1.0f;

    [Header("AI Behaviour Settings")]
    [SerializeField]
    private bool _enableHunt = false;
    [SerializeField]
    private bool _changeDirectionOnCollision = false;

    private int _movementTimer = 0;
    private int _movementLimit;

    private void Awake(){
        _movementLimit = _movementSequence;
        if (_changeDirectionOnCollision)
            RandomDiagonal();
    }

    private void FixedUpdate()
    {
        if (_enableHunt || _changeDirectionOnCollision)
            AIdirection();
        else
            RandomDirection();
        MainController();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_changeDirectionOnCollision)
            RandomDiagonal();

        StartCoroutine(RefreshBoxCo());
    }

    private IEnumerator RefreshBoxCo()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);

        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Object has a behaviour
    private void AIdirection()
    {
        if (!CheckSequence())
            return;

        if (_enableHunt)
        {
            if (_movementSequence > 5) _movementSequence = 2;
            HuntPlayer();
        }
        else if (_changeDirectionOnCollision)
            return;
    }

    // Object moves randomly
    private void RandomDirection()
    {
        if (!CheckSequence())
            return;

        if (_movementSequence <= 5)
            _movementSequence = _movementLimit;

        if (_enableDiagonal)
            RandomAllDirections();
        else
            RandomOneDirection();
    }

    // Directions Sub functions
    // Random direction on X & Y
    private void RandomAllDirections()
    {
        changePos.x = Random.Range(-1.0f, 1.0f);
        changePos.y = Random.Range(-1.0f, 1.0f);
    }
    // Random direction on X OR Y
    private void RandomOneDirection()
    {
        float direction = Random.Range(-1.0f, 1.1f);

        if (direction > 0)
            changePos.x = Random.Range(-1.0f, 1.0f);
        else
            changePos.y = Random.Range(-1.0f, 1.0f);
    }
    // Random diagonal direction
    public void RandomDiagonal()
    {
        if (changePos.x == 0)
        {
            if (GetRandomBool())
                changePos.x = 0.25f;
            else
                changePos.x = -0.25f;
        }
        else
            changePos.x = -1 * changePos.x;
        
        if (GetRandomBool())
            changePos.y = 0.25f;
        else
            changePos.y = -0.25f;
    }

    private bool GetRandomBool()
    {
        if (Random.Range(0, 2) == 1)
            return (true);

        return (false);
    }

    // AI functions
    // New direction depending of player
    private void HuntPlayer()
    {
        GameObject player   = GameObject.FindWithTag("Player");
        Transform target    = player.transform;
        Vector2 size        = player.GetComponent<BoxCollider2D>().size;

        if (Vector3.Distance(target.position, transform.position) < _chaseRadius)
        {
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

    private bool CheckSequence()
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
}
