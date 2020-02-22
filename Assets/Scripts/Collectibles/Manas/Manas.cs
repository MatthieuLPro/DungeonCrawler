using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Manas : Collectibles
{
    [HideInInspector]
    public int value = 0;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.transform.parent.GetComponent<Player>().GetMana(value);
        StartCoroutine(GetObjectFindEffectCo());
    }
}
