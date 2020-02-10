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

    override public void AddForceMovement(Vector3 directionVariation)
    {
        if(_waitTime == 0 || (!_IsWaiting && _IsMoving))
        {
            MoveObject(directionVariation);
            return;
        }

        LaunchCoroutines();
    }

    private void LaunchCoroutines()
    {
        if(_IsWaiting && !_IsMoving)
            StartCoroutine(WaitCo());
        else if (!_IsWaiting && !_IsMoving)
            StartCoroutine(MoveCo());
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* Is not moving */
    private IEnumerator WaitCo()
    {            
        _IsWaiting = true;

        yield return new WaitForSeconds(_waitTime);

        _IsWaiting = false;
    }

    /* Is moving */
    private IEnumerator MoveCo()
    {            
        _IsMoving  = true;

        yield return new WaitForSeconds(_moveTime);

        _IsMoving  = false;
    }
}
