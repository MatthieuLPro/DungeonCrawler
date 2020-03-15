using System.Collections;
using UnityEngine;

public class Rubies : Collectibles
{
    public int value;

    override public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        ResultPlayer resultPlayer = other.transform.parent.GetComponent<ResultPlayer>();
        resultPlayer.GetRuby(value);
        
        StartCoroutine(GetObjectFindEffectCo());
    }
}
