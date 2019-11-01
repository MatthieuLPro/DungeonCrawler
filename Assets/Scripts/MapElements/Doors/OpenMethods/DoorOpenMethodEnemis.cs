using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMethodEnemis : MonoBehaviour
{
    [Header("Select the enemis to kill")]
    [SerializeField]
    private GameObject[] _enemis = null;

    /* ************************************************ */
    /* Verify if all enemis are dead */
    /* ************************************************ */
    /* Called when an enemy is killed */

    private void VerificationEnemis()
    {
        if (_enemis.Length > 0)
            return;

        transform.parent.GetComponent<DoorsManager>().OpenMultipleDoors();
        Destroy(gameObject);
    }
}
