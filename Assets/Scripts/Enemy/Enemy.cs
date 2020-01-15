using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Main Settings")]
    [SerializeField]
    private int  _health     = 0;
    [SerializeField]
    private int  _mana       = 0;
    public bool isInvincible = false;

    private EnemyState _actualState;

    private int _actualHealth;
    private int _actualMana;

    public enum EnemyState {
        idle,
        move,
        knockBack,
        ko
    }

    void Start()
    {
        _actualHealth   = _health;
        _actualMana     = _mana;
        _actualState    = EnemyState.idle;
    }

    /* ************************************************ */
    /* Public Functions */
    /* ************************************************ */
    public void DamageHealth(int value)
    {
        _UpdateData(_actualHealth, value * -1);

        if (_IsDead())
            _LaunchDeathEffect();
    }

    public void HealLife(int value){
        _UpdateData(_actualHealth, value);
    }

    public void DamageMana(int value){
        _UpdateData(_actualMana, value * -1);
    }

    public void HealMana(int value){
        _UpdateData(_actualMana, value);
    }

    private void _LaunchDeathEffect(){
        StartCoroutine(_AnimationDeath());
    }

    /* ************************************************ */
    /* Updatter */
    /* ************************************************ */
    private void _UpdateData(int variable, int value){
        variable += value;
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
    public int GetHealth(){
        return _actualHealth;
    }
    public int GetMana(){
        return _actualMana;
    }

    public EnemyState GetState(){
        return _actualState;
    }

    /* ************************************************ */
    /* Question */
    /* ************************************************ */
    public bool IsInvincible(){
        return isInvincible;
    }

    private bool _IsDead(){
        return (_actualState <= 0);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    private IEnumerator _AnimationDeath()
    {
        GetComponent<Animator>().SetBool("Ko", true);
        yield return new WaitForSeconds(.59f);

        Destroy(gameObject); 
    }
}
