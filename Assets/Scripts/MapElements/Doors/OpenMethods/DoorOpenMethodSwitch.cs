using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMethodSwitch : MonoBehaviour
{
    /* ************************************************ */
    /* Open doors when hero enter in switch collider */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        //transform.parent.GetComponent<DoorsManager>().OpenMultipleDoors();
        Destroy(gameObject);        
    }
}
