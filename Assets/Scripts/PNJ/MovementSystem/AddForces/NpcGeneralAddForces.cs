using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralAddForces : MonoBehaviour
{
    [Header("General Settings")]
    public float speed;

    [HideInInspector]
    public Rigidbody2D rb2d;
    private GameObject _parent;

    private void Start()
    {
        _parent         = transform.parent.gameObject;
        rb2d            = _parent.GetComponent<Rigidbody2D>();
    }

    abstract public void AddForceMovement(Vector3 directionVariation);

    public void MoveObject(Vector3 directionVariation){
        rb2d.MovePosition(transform.position + directionVariation * speed * Time.deltaTime);
    }
}
