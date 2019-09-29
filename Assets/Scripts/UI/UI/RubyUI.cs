using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyUI : MonoBehaviour
{
    public static RubySystem rubySystemStatic;

    private int _ruby;
    private RubySystem _rubySystem;

    [Header("Attached player")]
    [SerializeField]
    private GameObject player;


    private void Start()
    {
        _rubySystem = new RubySystem(player.GetComponent<Player>().rubyInit);
        InitRubyUI();
    }

    public void InitRubyUI()
    {
        _ruby = player.GetComponent<Player>().rubyInit;
        GetComponent<Text>().text = _ruby.ToString();
        rubySystemStatic = _rubySystem;

        _rubySystem.OnDecrease += RefreshRuby;
        _rubySystem.OnIncrease += RefreshRuby;
    }

    private void RefreshRuby(object sender, System.EventArgs e){
        StartCoroutine(RubyCo());
    }

    private IEnumerator RubyCo()
    {
        int systemValue = _rubySystem.GetValue();
        if (_ruby < systemValue)
        {
            while(_ruby < systemValue)
            {
                _ruby++;
                GetComponent<Text>().text = _ruby.ToString();
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            while(_ruby > systemValue)
            {
                _ruby--;
                GetComponent<Text>().text = _ruby.ToString();
                yield return new WaitForSeconds(0.05f);

            }
        }
    }
}
