using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallKeyTextUI : MonoBehaviour
{
    public static SmallKeySystem smallKeySystemStatic;
    
    [HideInInspector]
    public SmallKeySystem smallKeySystem;

    private int _smallKey;
    private AudioSource _audio = null;

    [Header("Attached player")]
    [SerializeField]
    public GameObject _player;

    private void Start()
    {
        smallKeySystem = new SmallKeySystem(_player.GetComponent<Player>().keys);
        _audio = GetComponent<AudioSource>();
        InitSmallKeyUI();
    }

    public void InitSmallKeyUI()
    {
        _smallKey = _player.GetComponent<Player>().keys;
        GetComponent<Text>().text = _smallKey.ToString("0");
        smallKeySystemStatic = smallKeySystem;

        smallKeySystem.OnDecrease += RefreshSmallKey;
        smallKeySystem.OnIncrease += RefreshSmallKey;
    }

    private void RefreshSmallKey(object sender, System.EventArgs e){
        StartCoroutine(SmallKeyCo());
    }

    private IEnumerator SmallKeyCo()
    {
        int systemValue = smallKeySystem.GetValue();
        if (_smallKey < systemValue)
        {
            _smallKey++;
            GetComponent<Text>().text = _smallKey.ToString("0");
            yield return new WaitForSeconds(0.07f);
        }
        else
        {
            _smallKey--;
            GetComponent<Text>().text = _smallKey.ToString("0");
            yield return new WaitForSeconds(0.07f);
        }
    }
}
