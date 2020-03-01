using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private ObjectManager _manager;

    void Start(){
        _manager = transform.parent.GetComponent<ObjectManager>();
    }

    /* ************************************************ */
    /* OnTrigger interaction */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerAttack"))
            return;

        _manager.AnimationDestroy();
        _manager.ActiveSpriteRenderer();

        _manager.DesactivateMovementCollider();
        _manager.DesactivateDestructibleCollider();
        _manager.DesactivateCarrierCollider();

        _manager.DropCollectible();
    }
}
