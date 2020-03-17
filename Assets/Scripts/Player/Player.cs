using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player parameters")]
    public int playerIndex = 1;
    public int healthInit = 5;
    public int manaInit = 70;
    public int keys = 0;
    public bool bigKey = false;
    public int strength = 1;

    /* ************************************************ */
    /* Getters & Setters */
    /* ************************************************ */
    public int GetPlayerIndex() 
    {
        return playerIndex;
    }

    public void SetPlayerIndex(int index) 
    {
        playerIndex = index;
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
            _GetUIGO("Hearts", "HeartsHealthUI").GetComponent<HeartsHealthUI>().heartsHealthSystem.Heal(value);
        else
            _GetUIGO("Hearts", "HeartsHealthUI").GetComponent<HeartsHealthUI>().heartsHealthSystem.Damage(value);
    }

    private void _UpdateUIMana(bool adding, int value)
    {
        if (adding)
            _GetUIGO("Manas", "ManaUI").GetComponent<ManaUI>().manaSystem.ChangeMana(value);
        else
            _GetUIGO("Manas", "ManaUI").GetComponent<ManaUI>().manaSystem.ChangeMana(value * -1);
    }

    private void _UpdateUISmallKey(bool adding)
    {
        if (adding)
            _GetUIGO("SmallKeys", "SmallKeyTextUI").GetComponent<SmallKeyUI>().smallKeySystem.AddSmallKey();
        else
            _GetUIGO("SmallKeys", "SmallKeyTextUI").GetComponent<SmallKeyUI>().smallKeySystem.RemoveSmallKey();
    }

    private void _UpdateUIBigKey(bool adding)
    {
        if (adding)
            _GetUIGO("BigKeys", "BigKeyTextUI").GetComponent<BigKeyUI>().bigKeySystem.AddBigKey();
        else
            _GetUIGO("BigKeys", "BigKeyTextUI").GetComponent<BigKeyUI>().bigKeySystem.RemoveBigKey();
    }

    private Transform _GetUIGO(string fileName, string UIName){
        return transform.Find("UI").Find(fileName).Find(UIName);
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
