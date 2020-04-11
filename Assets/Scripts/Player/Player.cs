using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player parameters")]
    [SerializeField]
    private int _playerIndex = 1;
    public int healthInit = 5;
    public int manaInit = 116;
    public int keys = 0;
    public bool bigKey = false;
    public int strength = 1;

    /* ************************************************ */
    /* Getters & Setters */
    /* ************************************************ */
    public int PlayerIndex {
        get { return _playerIndex; }
        set { _playerIndex = value; }
    }

    public void GetLife(int heal){
        _UpdateUIHeart(true, heal);
    }

    public void LooseLife(int damage){
        _UpdateUIHeart(false, damage);
    }

    public void GetMana(int heal){
        _UpdateUIMana(true, heal);
    }

    public void LooseMana(int damage){
        _UpdateUIMana(false, damage);
    }

    public void GetSmallKey()
    {
        keys += 1;
        _UpdateUISmallKey(true);
    }

    public void LooseSmallKey()
    {
        keys -= 1;
        _UpdateUISmallKey(false);
    }
    
    public void GetBigKey()
    {
        bigKey = true;
        _UpdateUIBigKey(true);
    }

    public void LooseBigKey()
    {
        bigKey = false;
        _UpdateUIBigKey(false);
    }

    /* ************************************************ */
    /* Update player State */
    /* ************************************************ */
    public void IsDead(){
        Destroy(GetComponent<PlayerController>());
    }

    /* ************************************************ */
    /* Update UI */
    /* ************************************************ */
    private void _UpdateUIHeart(bool adding, int value)
    {
        if (adding)
            _GetUIGO("Hearts").GetComponent<HeartsHealthUI>().heartsHealthSystem.Heal(value);
        else
            _GetUIGO("Hearts").GetComponent<HeartsHealthUI>().heartsHealthSystem.Damage(value);
    }

    private void _UpdateUIMana(bool adding, int value)
    {
        if (adding)
            _GetUIGO("Manas").GetComponent<ManaUI>().manaSystem.ChangeMana(value);
        else
            _GetUIGO("Manas").GetComponent<ManaUI>().manaSystem.ChangeMana(value * -1);
    }

    private void _UpdateUISmallKey(bool adding)
    {
        if (adding)
            _GetUIGO("SmallKeyTextUI", "SmallKeys").GetComponent<SmallKeyTextUI>().smallKeySystem.AddSmallKey();
        else
            _GetUIGO("SmallKeyTextUI", "SmallKeys").GetComponent<SmallKeyTextUI>().smallKeySystem.RemoveSmallKey();
    }

    private void _UpdateUIBigKey(bool adding)
    {
        if (adding)
            _GetUIGO("BigKeys").GetComponent<BigKeyIconUI>().UpdateIcon(true);
        else
            _GetUIGO("BigKeys").GetComponent<BigKeyIconUI>().UpdateIcon(false);
    }

    private Transform _GetUIGO(string UIName, string fileName = ""){
        if (fileName != "")
            return transform.Find("UI").transform.Find(fileName).transform.Find(UIName);
        else
            return transform.Find("UI").transform.Find(UIName);
    }

    /* ************************************************ */
    /* Predicates */
    /* ************************************************ */
    public bool HasSmallKey()
    {
        if (keys <= 0) return (false);
        return (true);
    }

    public bool HasBigKey()
    {
        if (bigKey == false) return (false);
        return (true);
    }

    /* ************************************************ */
    /* Getters */
    /* ************************************************ */
    public int GetStrength(){
        return strength;
    }
}
