using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMovements : NpcGeneralAddForces
{
    [Header("Sequence Settings")]
    [SerializeField]
    private float _waitTime = .0f;
    [SerializeField]
    private float _moveTime = .0f;

    private bool _isWaiting = true;
    private bool _isMoving  = false;

    override protected void AddForceMovement()
    {
        if(_waitTime == 0 || (!_isWaiting && _isMoving))
        {
            MoveObject();
            return;
        }

        LaunchCoroutines();
    }

    private void LaunchCoroutines()
    {
        if(_isWaiting && !_isMoving)
            StartCoroutine(WaitCo());
        else if (!_isWaiting && !_isMoving)
            StartCoroutine(MoveCo());
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* Is not moving */
    private IEnumerator WaitCo()
    {            
        _isWaiting = true;

        yield return new WaitForSeconds(_waitTime);

        _isWaiting = false;
    }

    /* Is moving */
    private IEnumerator MoveCo()
    {            
        _isMoving  = true;

        yield return new WaitForSeconds(_moveTime);

        _isMoving  = false;
    }
}
