using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovingObject
{
    // AI
    [SerializeField]
    private bool _enableDiagonal    = true,
                 _enableHunt        = false;

    // Characteristics
    [SerializeField]
    private int _movementLength = 50;

    [SerializeField]
    private float _chaseRadius = 1.0f;

    private int _movementTimer = 0;
    private int _movementLimit;

    void Awake(){
        _movementLimit = _movementLength;
    }

    void FixedUpdate()
    {
        EnemyDirection();
        MainController();
    }

    public Vector3 GetPosition(){
        return (transform.position);
    }

    private void EnemyDirection()
    {
        if (_movementTimer <= _movementLength)
        {
            _movementTimer++;
            return;
        }
        _movementTimer = 0;
        changePos = Vector3.zero;

        if (_enableHunt)
        {
            if (_movementLength > 5) _movementLength = 2;
            FollowPlayer();
        }
        else if (_enableDiagonal)
        {
            if (_movementLength <= 5) _movementLength = _movementLimit;
            ChangeDirection();
        }
        else
        {
            if (_movementLength <= 5) _movementLength = _movementLimit;
            ChangeDirectionXorY();
        }
    }

    private void ChangeDirection()
    {
        changePos.x = Random.Range(-1.0f, 1.0f);
        changePos.y = Random.Range(-1.0f, 1.0f);
    }

    private void ChangeDirectionXorY()
    {
        float direction = Random.Range(-1.0f, 1.1f);

        if (direction > 0)
            changePos.x = Random.Range(-1.0f, 1.0f);
        else
            changePos.y = Random.Range(-1.0f, 1.0f);
    }

    private void FollowPlayer()
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
}