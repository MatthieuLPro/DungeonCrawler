using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wall"))
            return;

        Destroy(transform.parent.gameObject);
    }
}
