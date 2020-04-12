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

    private void Start()
    {
        _smallKey      = transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<Player>().Keys;
        smallKeySystem = new SmallKeySystem(_smallKey);
        InitSmallKeyUI();
    }

    public void InitSmallKeyUI()
    {
        _RefreshTextUI();
        smallKeySystemStatic      = smallKeySystem;

        smallKeySystem.OnDecrease += _RefreshSmallKey;
        smallKeySystem.OnIncrease += _RefreshSmallKey;
    }

    void _RefreshSmallKey(object sender, System.EventArgs e){
        StartCoroutine(_SmallKeyCo());
    }

    IEnumerator _SmallKeyCo()
    {
        int systemValue = smallKeySystem.GetValue();
        if (_smallKey < systemValue)
            _smallKey++;
        else
            _smallKey--;
        _RefreshTextUI();
        yield return new WaitForSeconds(0.07f);
    }

    void _RefreshTextUI() {
        GetComponent<Text>().text = _smallKey.ToString("0");
    }
}
