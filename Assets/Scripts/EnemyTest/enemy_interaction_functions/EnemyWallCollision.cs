using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCollision : MonoBehaviour
{
    [Header("Behaviour options")]
    [SerializeField]
    private string _collisionBehaviour = "direction";

    private void OnCollisionEnter2D(Collision2D t)
    {
        if (_collisionBehaviour == "direction")
            _ChangeDirection(t.GetContact(0).normal);
        else if (_collisionBehaviour == "direction_opposite")
            _ChangeOppositeDirection();

        StartCoroutine(_RefreshColliderCo());
    }

    private void _ChangeDirection(Vector3 contact) {
        DeterminateSingleDirections direction = transform.parent.GetChild(0).transform.GetChild(0).GetComponent<DeterminateSingleDirections>();
        float xDirection = ((contact.x < 0.1f && contact.x > -0.1f) ? Random.Range(0, 2) : direction.DirectionX * -1);
        float yDirection = ((contact.y < 0.1f && contact.y > -0.1f) ? Random.Range(0, 2) : direction.DirectionY * -1);
        
        if (xDirection == 0)
            direction.DirectionX = -0.5f;
        else if (xDirection == 1)
            direction.DirectionX = 0.5f;
        else
            direction.DirectionX = xDirection;

        if (yDirection == 0)
            direction.DirectionY = -0.5f;
        else if (yDirection == 1)
            direction.DirectionY = 0.5f;
        else
            direction.DirectionY = yDirection;
    }

    private void _ChangeOppositeDirection() {
        DeterminateSingleDirections direction = transform.parent.GetChild(0).transform.GetChild(0).GetComponent<DeterminateSingleDirections>();
        direction.DirectionX = direction.DirectionX * -1;
        direction.DirectionY = direction.DirectionY * -1;
    }

    private IEnumerator _RefreshColliderCo() {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
