using System.Collections;
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
    private ResultPlayer _resultPlayer;

    private void Start()
    {
        _resultPlayer = transform.parent.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<ResultPlayer>();
        rubySystem = new RubySystem(_resultPlayer.rubyInit);
        _audio = GetComponent<AudioSource>();
        InitRubyUI();
    }

    public void InitRubyUI()
    {
        _ruby = _resultPlayer.rubyInit;
        GetComponent<Text>().text = _ruby.ToString("00");
        rubySystemStatic = rubySystem;

        rubySystem.OnDecrease += RefreshRuby;
        rubySystem.OnIncrease += RefreshRuby;
    }

    private void RefreshRuby(object sender, System.EventArgs e){
        StartCoroutine(RubyCo());
    }

    private IEnumerator RubyCo()
    {
        int systemValue = rubySystem.GetValue();
        if (_ruby < systemValue)
        {
            while(_ruby < systemValue)
            {
                _audio.Play();
                _ruby++;
                GetComponent<Text>().text = _ruby.ToString("00");
                yield return new WaitForSeconds(0.07f);
            }
        }
        else
        {
            while(_ruby > systemValue)
            {
                _audio.Play();
                _ruby--;
                GetComponent<Text>().text = _ruby.ToString("00");
                yield return new WaitForSeconds(0.07f);

            }
        }
    }
}
