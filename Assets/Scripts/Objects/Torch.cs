using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [Header("Torch Parameter")]
    [SerializeField]
    private bool _infiniteLamp = false;
    [SerializeField]
    private bool _lightOn = false;

    private Animator _anime = null;
    private SpriteRenderer _spriteRend = null;

    void Start()
    {
        _anime      = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
        
        if (_infiniteLamp)
            LightInfinite();
        else if (_lightOn)
            LightOn();
    }

    private void LightOn(){
        StartCoroutine(LightCo());
    } 

    private void LightOff()
    {
        if (!_lightOn)
            return;

        _lightOn = false;
        _anime.SetBool("LightOn", false);

    }

    private void LightInfinite()
    {
        _lightOn = true;
        _anime.SetBool("LightOn", true);
    }

    private IEnumerator LightCo()
    {
        _lightOn = true;
        _anime.SetBool("LightOn", true);
        yield return new WaitForSeconds(10);

        _lightOn = false;
        _anime.SetBool("LightOn", false);
    }


}
