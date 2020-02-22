using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallKeys : Collectibles
{
    override public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        other.transform.parent.GetComponent<Player>().GetSmallKey();
        StartCoroutine(GetObjectFindEffectCo());
    }
}
