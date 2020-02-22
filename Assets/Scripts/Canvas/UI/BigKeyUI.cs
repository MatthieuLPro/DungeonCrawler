using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigKeyUI : MonoBehaviour
{
    public static BigKeySystem bigKeySystemStatic;
    
    [HideInInspector]
    public BigKeySystem bigKeySystem;

    private bool _bigKey;
    private AudioSource _audio = null;

    [Header("Attached player")]
    [SerializeField]
    public GameObject _player;
    

    private void Start()
    {
        smallKeySystem = new SmallKeySystem(_player.GetComponent<Player>().HasBigKey());
        _audio = GetComponent<AudioSource>();
        InitSmallKeyUI();
    }

    public void InitSmallKeyUI()
    {
        _bigKey = _player.GetComponent<Player>().HasBigKey();
        char value = 'X';

        if (_bigKey)
            value = 'O';
        GetComponent<Text>().text = _smallKey.ToString();
        smallKeySystemStatic = smallKeySystem;

        smallKeySystem.OnDecrease += RefreshSmallKey;
        smallKeySystem.OnIncrease += RefreshSmallKey;
    }

    private char _GetCharValue()
    {
        char value = 'X';

        if (_bigKey) value = 'O';
        return value;
    }

    private void RefreshSmallKey(object sender, System.EventArgs e){
        StartCoroutine(SmallKeyCo());
    }

    private IEnumerator SmallKeyCo()
    {
        int systemValue = smallKeySystem.GetValue();
        if (_smallKey < systemValue)
        {
            while(_smallKey < systemValue)
            {
                _smallKey++;
                GetComponent<Text>().text = _smallKey.ToString();
                yield return new WaitForSeconds(0.07f);
            }
        }
        else
        {
            while(_smallKey > systemValue)
            {
                _smallKey--;
                GetComponent<Text>().text = _smallKey.ToString();
                yield return new WaitForSeconds(0.07f);

            }
        }
    }
}
