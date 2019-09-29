using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruby : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    private void Start(){
        SetRubyValue();
    }

    private void SetRubyValue()
    {
        Animator anime = GetComponent<Animator>();
        if(value == 1)
            anime.SetInteger("Color", 0);
        else if (value == 5)
            anime.SetInteger("Color", 1);
        else
            anime.SetInteger("Color", 2);
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
}
