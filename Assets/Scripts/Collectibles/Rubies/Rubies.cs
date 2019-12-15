using System.Collections;
using UnityEngine;

abstract public class Rubies : MonoBehaviour
{
    public int          value;
    private AudioSource _audio = null;

    private void Start(){
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.GetRuby(value);
            Destroy(gameObject);
        }     
    }

    private IEnumerator GetRubyCo()
    {
        _audio.Play();
        yield return new WaitForSeconds(0.2f);
        
        Destroy(gameObject);
    }
}
