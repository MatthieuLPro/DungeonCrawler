using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    public static ManaSystem manaSystemStatic;
    public ManaSystem manaSystem;

    [SerializeField]
    private Sprite _manaBarSprite = null;

    private GameObject _manaBar;

    [Header("Attached player")]
    [SerializeField]
    private GameObject player = null;


    private void Start(){
        _manaBar    = new GameObject("manaBar", typeof(Image));
        manaSystem = new ManaSystem(player.GetComponent<Player>().manaInit);
        ManaDisplay();
    }

    private void ManaDisplay()
    {
        Image manaBarUI = _manaBar.GetComponent<Image>();

        manaBarUI.sprite = _manaBarSprite;
        InitManaUI();
    }

    public void InitManaUI()
    {
        int manaValue = manaSystem.GetMana();
        manaSystemStatic = manaSystem;

        _manaBar.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
        SetPositionManaUI(manaValue);
    
        manaSystem.OnDecrease += RefreshMana;
        manaSystem.OnIncrease += RefreshMana;
    }

    private void RefreshMana(object sender, System.EventArgs e)
    {
        int manaValue = manaSystem.GetMana();

        SetPositionManaUI(manaValue);
    }

    private void SetPositionManaUI(int manaValue)
    {
        _manaBar.transform.SetParent(transform);
        _manaBar.transform.localPosition = Vector3.zero;
        _manaBar.transform.localScale = new Vector3(1, 1, 1);
        _manaBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -18);
        _manaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(10, manaValue);
    }
}
