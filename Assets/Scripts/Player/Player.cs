using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player System parameters")]
    [SerializeField]
    private int _playerIndex = 1;

    [Header("Player Characteristics parameters")]
    [SerializeField]
    private int _healthInit = 5;
    [SerializeField]
    private int _manaInit = 116;
    [SerializeField]
    private int _strength = 1;
    [SerializeField]
    private int _defense = 1;
    [SerializeField]
    private int _speed = 1;

    [Header("Player Game parameters")]
    [SerializeField]
    private int _keys = 0;
    [SerializeField]
    private bool _bigKey = false;

    // Loosing stamina on the time
    public bool _isLoosingStamina = false;
    public bool _isLoosingStaminaCoIsOn = false;
    public float _loosingStaminaSpeed = 1f;
    public int _loosingStaminaDamage = 1;

    // Loosing health on the time
    public bool _isLoosingLife = false;
    public bool _isLoosingLifeCoIsOn = false;
    public float _loosingLifeSpeed = 1f;
    public int _loosingLifeDamage = 1;

    /* ************************************************ */
    /* Test Stamina */
    /* ************************************************ */

    void Update() {
        if (!_isLoosingStaminaCoIsOn && _isLoosingStamina)
            StartCoroutine(_IsLoosingStaminaCo());
        if (!_isLoosingLifeCoIsOn && _isLoosingLife)
            StartCoroutine(_IsLoosingLifeCo());
    }

    private IEnumerator _IsLoosingStaminaCo() {
        _isLoosingStaminaCoIsOn = true;
        yield return new WaitForSeconds(_loosingStaminaSpeed);
        LooseMana(_loosingStaminaDamage);
        _isLoosingStaminaCoIsOn = false;
    }

    private IEnumerator _IsLoosingLifeCo() {
        _isLoosingLifeCoIsOn = true;
        yield return new WaitForSeconds(_loosingLifeSpeed);
        LooseLife(_loosingLifeDamage);
        _isLoosingLifeCoIsOn = false;
    }

    /* ************************************************ */
    /* Update status */
    /* ************************************************ */

    public void GainLife(int heal){
        _UpdateUIHeart(true, heal);
    }

    public void LooseLife(int damage){
        _UpdateUIHeart(false, damage);
    }

    public void GainMana(int heal){
        _UpdateUIMana(true, heal);
    }

    public void LooseMana(int damage){
        _UpdateUIMana(false, damage);
    }

    public void GainSmallKey(){
        Keys = 1;
        _UpdateUISmallKey(true);
    }

    public void LooseSmallKey(){
        Keys = -1;
        _UpdateUISmallKey(false);
    }
    
    public void GainBigKey(){
        BigKey = true;
        _UpdateUIBigKey(true);
    }

    public void LooseBigKey(){
        BigKey = false;
        _UpdateUIBigKey(false);
    }

    /* ************************************************ */
    /* Update player State */
    /* ************************************************ */
    public void IsDead(){
        Destroy(gameObject);
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
            return transform.Find("UI").Find("TopLeft").transform.Find(fileName).transform.Find(UIName);
        else
            return transform.Find("UI").Find("TopLeft").transform.Find(UIName);
    }

    /* ************************************************ */
    /* Predicates */
    /* ************************************************ */
    public bool HasSmallKey(){
        return (Keys > 0);
    }

    public bool HasBigKey(){
        return (BigKey);
    }

    /* ************************************************ */
    /* Getters */
    /* ************************************************ */
    public int PlayerIndex {
        get => _playerIndex;
        set => _playerIndex = value;
    }

    public int HealthInit {
        get => _healthInit;
    }

    public int ManaInit {
        get => _manaInit;
    }

    public int Keys {
        get => _keys;
        set => _keys += value;
    }

    public bool BigKey {
        get => _bigKey;
        set => _bigKey = value;
    }

    public int Strength{
        get => _strength;
        set => _strength = value;
    }

    public int Defense{
        get => _defense;
        set => _defense = value;
    }

    public int Speed{
        get => _speed;
        set => _speed = value;
    }
}
