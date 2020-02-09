using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class NpcGeneralAddForces : MonoBehaviour
{
    [Header("General Settings")]
    public float   speed;

    [HideInInspector]
    public Vector2 DirectionVariation { get; set; }
    [HideInInspector]
    public Rigidbody2D rb2d;
    private GameObject _parent;
    private NpcGeneralDirections _directions;

    private void Start()
    {
        _parent         = transform.parent.gameObject;
        _directions     = _parent.GetChild(0).GetComponent<NpcGeneralDirections>();
        rb2d            = _parent.GetComponent<Rigidbody2D>();

        if (_wakeSystem)
            _isWaiting = true;
    }

    private void MoveObject()
    {
        _directions.UpdatePosition();

        DirectionVariation = _directions.PosVariation();
        DirectionVariation = DirectionVariation.Normalize();

        rb2d.MovePosition(transform.position + DirectionVariation * speed * Time.deltaTime);
    }

    abstract protected void AddForceMovement();
}
