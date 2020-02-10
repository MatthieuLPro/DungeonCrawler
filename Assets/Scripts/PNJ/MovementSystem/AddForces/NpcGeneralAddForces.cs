using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralAddForces : MonoBehaviour
{
    [Header("General Settings")]
    private float _speed;

    private Rigidbody2D _rb2d;
    private GameObject _parent;

    [HideInInspector]
    public bool isWaiting = true;
    [HideInInspector]
    public bool isMoving  = false;

    public float _Speed { get; set; }

    private void Start()
    {
        _parent = transform.parent.gameObject.transform.parent.gameObject;
        _rb2d   = _parent.GetComponent<Rigidbody2D>();
    }

    abstract public void AddForceMovement(Vector3 directionVariation);

    public void MoveObject(Vector3 directionVariation){
        _rb2d.MovePosition(transform.position + directionVariation * _Speed * Time.deltaTime);
    }
}
