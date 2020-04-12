using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Hearts : Collectibles
{
    [HideInInspector]
    public int value = 0;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.transform.parent.GetComponent<Player>().GainLife(value);
        StartCoroutine(GetObjectFindEffectCo());
    }
}
