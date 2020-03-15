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
        bigKeySystem = new BigKeySystem(_player.GetComponent<Player>().HasBigKey());
        _audio = GetComponent<AudioSource>();
        InitSmallKeyUI();
    }

    public void InitSmallKeyUI()
    {
        _bigKey = _HasBigKey();
        GetComponent<Text>().text = _GetCharValue();
        bigKeySystemStatic = bigKeySystem;

        bigKeySystem.OnDecrease += RefreshBigKey;
        bigKeySystem.OnIncrease += RefreshBigKey;
    }

    private bool _HasBigKey(){
        return _player.GetComponent<Player>().HasBigKey();
    }

    private string _GetCharValue()
    {
        string value = "X";

        if (_bigKey) value = "O";
        return value;
    }

    private Color _GetColor()
    {
        Color value = Color.red;

        if (_bigKey) value = Color.green;
        return value;
    }

    private void RefreshBigKey(object sender, System.EventArgs e){
        StartCoroutine(BigKeyCo());
    }

    private IEnumerator BigKeyCo()
    {
        _bigKey = _HasBigKey();
        GetComponent<Text>().text   = _GetCharValue();
        GetComponent<Text>().color  = _GetColor();
        yield return new WaitForSeconds(0.07f);
    }
}
