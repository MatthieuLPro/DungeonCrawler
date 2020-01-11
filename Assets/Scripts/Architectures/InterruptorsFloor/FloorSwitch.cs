using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour
{
    [HideInInspector]
    public bool value = false;

    /* ************************************************ */
    /* Change DoorSwitch value when hero enter in collider */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        value = !value;
        GetComponent<AudioSource>().Play();
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
}
