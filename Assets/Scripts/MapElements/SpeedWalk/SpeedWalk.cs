using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWalk : MonoBehaviour
{
    [Header("SpeedWalk Settings")]
    [SerializeField]
    private float _thrust = .0f;
    [SerializeField]
    private Vector3 _vectorDirection;

    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Enter");
        other.GetComponent<Rigidbody2D>().AddForce(_vectorDirection * _thrust, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D other){
        Debug.Log("Exit");
        other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
