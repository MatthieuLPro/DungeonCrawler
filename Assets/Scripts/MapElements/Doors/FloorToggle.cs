using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorToggle : MonoBehaviour
{
    [HideInInspector]
    public bool value = false;

    private bool _coIsWorking = false;

    /* ************************************************ */
    /* Toggle floor switch value when hero enter in collider */
    /* ************************************************ */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_coIsWorking)
            return;

        if (!other.CompareTag("Player"))
            return;

        StartCoroutine(toggleCo());
        value = !value;
    }

    private IEnumerator toggleCo()
    {
        _coIsWorking = true;

        yield return new WaitForSeconds(1);

        _coIsWorking = false;
    }
}
