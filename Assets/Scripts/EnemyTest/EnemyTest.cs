using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    /* ////////////////// */
    // Alphabetical Order
    /* ////////////////// */
    [Header("Health & Mana")]
    public int maxHealth;
    public int maxMana;

    [Header("Attack interaction")]
    public bool  attackTypeMagic;
    public bool  attackTypePhysic;
    public float knockBackTime;
    public int strength;
    public float thrust;

    private int _actualHealth;
    private int _actualMana;

    public enum EnemyState {
        idle,
        move,
        knockBack,
        ko
    }

    private EnemyState _actualState;

    void Start()
    {
        _actualHealth    = maxHealth;
        _actualMana      = maxMana;
        _actualState           = EnemyState.idle;

    }

    /* ************************************************ */
    /* Public functions */
    /* ************************************************ */  
    public void DamageHealth(int value)
    {
        _UpdateData(_actualHealth, value * -1);

        if (_IsDead())
            _AnimationDeath();
    }
  
    public void Healhealth(int value)
    {
        _UpdateData(_actualHealth, value);
        _UpdateForLimits(_actualHealth, maxHealth);
    }

    public void DamageMana(int value){
        _UpdateData(_actualMana, value * -1);
    }

    public void HealMana(int value)
    {
        _UpdateData(_actualMana, value);
        _UpdateForLimits(_actualMana, maxMana);
    }

    /* ************************************************ */
    /* Updatter */
    /* ************************************************ */
    private void _UpdateData(int variable, int value){
        variable += value;
    }

    private void _UpdateForLimits(int variable, int limit)
    {
        if (variable > limit)
            variable = limit;

        if (variable < 0)
            variable = 0;
    }

    /* ************************************************ */
    /* Setter */
    /* ************************************************ */
    public void SetState(string state)
    {
        switch (state)
        {
            case "idle":
                _actualState = EnemyState.idle;
                break;
            case "move":
                _actualState = EnemyState.move;
                break;
            case "knockBack":
                _actualState = EnemyState.knockBack;
                break;
            case "ko":
                _actualState = EnemyState.ko;
                break;
            default:
                _actualState = EnemyState.idle;
                break;
        }
    }

    /* ************************************************ */
    /* Getter */
    /* ************************************************ */
    public int GetActualHealth(){
        return _actualHealth;
    }
    public int GetActualMana(){
        return _actualMana;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }
    public int GetMaxMana(){
        return maxMana;
    }

    public EnemyState GetState(){
        return _actualState;
    }

    public int GetStrength(){
        return strength;
    }

    public float GetThrust(){
        return thrust;
    }

    public float GetKnockBackTime(){
        return knockBackTime;
    }

    public bool GetAttackTypePhysic(){
        return attackTypePhysic;
    }    

    public bool GetAttackTypeMagic(){
        return attackTypeMagic;
    }

    /* ************************************************ */
    /* Question */
    /* ************************************************ */
    private bool _IsDead(){
        return (_actualHealth <= 0);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    private IEnumerator _AnimationDeath()
    {
        GetComponent<Animator>().SetBool("Ko", true);

        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(2).GetComponent<AudioManager>().CallAudio("Ko");

        yield return new WaitForSeconds(.59f);

        Destroy(gameObject); 
    }
}
