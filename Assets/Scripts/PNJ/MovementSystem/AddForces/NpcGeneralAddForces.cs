using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralAddForces : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rb2d;
    private GameObject _parent;

    private bool _isWaiting = true;
    private bool _isMoving  = false;


    // Getter & setter
    public float _Speed { get { return _speed; } set { _speed = value; } }
    public bool _IsWaiting { get { return _isWaiting; } set { _isWaiting = value; } }
    public bool _IsMoving { get { return _isMoving; } set { _isMoving = value; } }

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
