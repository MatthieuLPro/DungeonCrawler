using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Keys : MonoBehaviour
{
    abstract public void OnTriggerEnter2D(Collider2D other);

    //=== Coroutine Destroy object ===    
    protected IEnumerator GetObjectFindEffectCo()
    {
        _DisableObject();
        _PlayAudio();
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }

    //=== Disable object and play Audio ===  
    private void _DisableObject()
    {
        GetComponent<BoxCollider2D>().enabled  = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void _PlayAudio(){
        GetComponent<AudioSource>().Play();
    }
}
