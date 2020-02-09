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

    // Abstract method
    override protected Vector2 GetDirectionVariations()
    {
        GameObject target          = GameObject.FindWithTag("Player"); // Change this later by _GetClosestTarget()
        Vector2 targetPosition     = target.transform.position;
        Vector2 targetColliderSize = target.GetComponent<BoxCollider2D>().size;

        if (_TargetIsTooClose(targetPosition))
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

    // Verification direction
    private bool _TargetIsTooClose(Vector2 targetPosition)
    {
        if (_chaseRadius <= 0 || _chaseLength <= 0)
            return false;

        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance < _chaseRadius ||
            distance > _chaseLength || _chaseLength == 0)
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
