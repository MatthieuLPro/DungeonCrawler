using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColor : MonoBehaviour
{
    [Header("Switch Color Settings")]
    public bool color = false;
    public Sprite redSprite = null;
    public Sprite blueSprite = null;

    private bool _coIsWorking = false;

    /*
        if color == true
            sprite = redSprite;
        else
            sprite = blueSprite;
     */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_coIsWorking)
            return;

        if (!other.CompareTag("Sword"))
            return;

        StartCoroutine(ChangeColorCo());
        color = !color;
    }

    private IEnumerator ChangeColorCo()
    {
        _coIsWorking = true;

        yield return new WaitForSeconds(0.5f);

        _coIsWorking = false;
    }
}
