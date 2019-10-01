using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Set Teleport Arrival")]
    [SerializeField]
    private GameObject _teleportArrival = null;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        other.GetComponent<Transform>().position = _teleportArrival.GetComponent<Transform>().position;
    }
}
