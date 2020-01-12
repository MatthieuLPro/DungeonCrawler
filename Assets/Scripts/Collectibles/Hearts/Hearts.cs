using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Hearts : MonoBehaviour
{
    [HideInInspector]
    public int heal = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.transform.parent.GetComponent<Player>().GetLife(heal);
        StartCoroutine(GetObjectFindEffectCo());
    }

    private IEnumerator GetObjectFindEffectCo()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}
