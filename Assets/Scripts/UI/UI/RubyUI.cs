﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyUI : MonoBehaviour
{
    public static RubySystem rubySystemStatic;
    public RubySystem rubySystem;

    private int _ruby;
    private AudioSource _audio = null;

    [Header("Attached result player")]
    [SerializeField]
    public GameObject _resultPlayer;
    

    private void Start()
    {
        rubySystem = new RubySystem(_resultPlayer.GetComponent<ResultPlayer>().rubyInit);
        _audio = GetComponent<AudioSource>();
        InitRubyUI();
    }

    public void InitRubyUI()
    {
        _ruby = _resultPlayer.GetComponent<ResultPlayer>().rubyInit;
        GetComponent<Text>().text = _ruby.ToString();
        rubySystemStatic = rubySystem;

        rubySystem.OnDecrease += RefreshRuby;
        rubySystem.OnIncrease += RefreshRuby;
    }

    private void RefreshRuby(object sender, System.EventArgs e){
        StartCoroutine(RubyCo());
    }

    private IEnumerator RubyCo()
    {
        Debug.Log("player: " + _resultPlayer.transform.parent.name);
        int systemValue = rubySystem.GetValue();
        if (_ruby < systemValue)
        {
            while(_ruby < systemValue)
            {
                _audio.Play();
                _ruby++;
                GetComponent<Text>().text = _ruby.ToString();
                yield return new WaitForSeconds(0.07f);
            }
        }
        else
        {
            while(_ruby > systemValue)
            {
                _audio.Play();
                _ruby--;
                GetComponent<Text>().text = _ruby.ToString();
                yield return new WaitForSeconds(0.07f);

            }
        }
    }
}
