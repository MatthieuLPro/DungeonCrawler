using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ************************************************ */
/* New directions for direction type HUNTER */
/* ************************************************ */
/* If _enableHunt == true */
// New direction depending of target position
abstract public class HuntDirections : NpcGeneralDirections
{
    [Header("Hunt Direction Settings")]
    [SerializeField]
    private float _chaseRadius = 0.0f;
    [SerializeField]
    private float _chaseLength = 0.0f;

    public HuntDirections()
    {
        if(_chaseRadius <= 0)
            _chaseRadius = 1;

        if(_chaseLength <= 0)
            _chaseLength = 0.05f;
    }

    // Abstract method
    override protected Vector2 GetDirectionVariations()
    {
        GameObject target          = GameObject.FindWithTag("Player"); // Change this later by _GetClosestTarget()
        Vector2 targetPosition     = target.transform.position;
        Vector2 targetColliderSize = target.GetComponent<BoxCollider2D>().size;

        float distance = Vector3.Distance(targetPosition, transform.position);

        if (_TargetIsTooClose(distance))
            return _SetNotMoving();

        if (_TargetIsTooFar(distance))
            return _SetNotMoving();

        return _GetDirection(targetPosition, targetColliderSize);
    }

    private void _GetClosestTarget()
    {
        // Area of hunt
        // Find the first closest target
        // return null if no object is close
        // else return object
    }

    // Verification target position
    private bool _TargetIsTooClose(float distance)
    {
        if (distance <= _chaseLength)
            return true;

        return false;
    }

    private bool _TargetIsTooFar(float distance)
    {
        if (distance > _chaseRadius)
            return true;

        return false;
    }

    // Set direction to zero
    private Vector2 _SetNotMoving(){
        return Vector2.zero;
    }

    // Set direction variation
    abstract protected Vector2 _GetDirection(Vector2 targetPosition, Vector2 playerColliderSize);

    protected float _GetDirectionVariation(float targetPositionValue, float playerColliderSize)
    {
        if (targetPositionValue > targetPositionValue + playerColliderSize)
            return -1.0f;
        else if (targetPositionValue < targetPositionValue - playerColliderSize)
            return 1.0f;

        return 0.0f;
    }
}
