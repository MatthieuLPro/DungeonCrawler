using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOscillateMvt : MonoBehaviour
{
    private Vector3 _parentPos = Vector3.zero;

    [Header("Position params: ")]
    [SerializeField]
    private Vector3 _offsetPos = Vector3.zero;

    [Header("Movement params: ")]
    [SerializeField]
    private float _hoverHeight = 1.0f;
    [SerializeField]
    private float _hoverRange = 0.1f;
    [SerializeField]
    private float _hoverSpeed = 10.0f;

    void Start() {
        _parentPos = transform.parent.transform.position;
        _offsetPos = new Vector3(0, 0.6f, 0);
    } 

    void Update() {
        this.transform.position = _parentPos + _offsetPos + Vector3.up * _hoverHeight * Mathf.Cos(Time.time * _hoverSpeed) * _hoverRange;
    }
}
