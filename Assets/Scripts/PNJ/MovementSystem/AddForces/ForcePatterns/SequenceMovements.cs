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
        if(_waitTime == 0 || (!isWaiting && isMoving))
        {
            MoveObject(directionVariation);
            return;
        }

        LaunchCoroutines();
    }

    private void LaunchCoroutines()
    {
        if(isWaiting && !isMoving)
            StartCoroutine(WaitCo());
        else if (!isWaiting && !isMoving)
            StartCoroutine(MoveCo());
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* Is not moving */
    private IEnumerator WaitCo()
    {            
        isWaiting = true;

        yield return new WaitForSeconds(_waitTime);

        isWaiting = false;
    }

    /* Is moving */
    private IEnumerator MoveCo()
    {            
        isMoving  = true;

        yield return new WaitForSeconds(_moveTime);

        isMoving  = false;
    }
}
