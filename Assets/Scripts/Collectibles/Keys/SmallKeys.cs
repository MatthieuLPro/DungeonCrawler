using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallKeys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        other.transform.parent.GetComponent<Player>().GetSmallKey();
        StartCoroutine(GetObjectFindEffectCo());
    }

    private IEnumerator GetObjectFindEffectCo()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}
