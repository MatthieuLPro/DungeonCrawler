using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOscillateMvt : MonoBehaviour
{
    private Vector3 _parentPos = Vector3.zero;

    float maxHeight = 0.5f;
    float minHeight = -0.5f;
    float hoverHeight = 0.0f;
    float hoverRange = 0.0f;
    float hoverSpeed = 10.0f;

    void Start() {
        _parentPos = transform.parent.transform.position;
        hoverHeight = (maxHeight + minHeight) / 2.0f;
        hoverRange = maxHeight - minHeight;
    } 

    void Update() {
        this.transform.position = _parentPos + Vector3.up * hoverHeight * Mathf.Cos(Time.time * hoverSpeed) * hoverRange;
    }
}
