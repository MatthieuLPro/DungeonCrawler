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

    private int _actualHealth;
    private int _actualMana;

    void Start(){
        _actualHealth = _health;
        _actualMana = _mana;
    }

    /* ************************************************ */
    /* Change enemy values */
    /* ************************************************ */
    public void ChangeHealth(int value){
        if (_actualHealth + value > _health)
            _actualHealth = _health;
        else
            _actualHealth += value;
        IsDead();
    }

    public void ChangeMana(int value){
        if(_actualMana + value > _mana)
            _actualMana = _mana;
        else if (_actualMana + value < 0)
            _actualMana = 0;
        else
            _actualMana += value;
    }

    public void IsDead(){
        if (_actualHealth <= 0)
            StartCoroutine(AnimationDeath());
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    private IEnumerator AnimationDeath()
    {
        GetComponent<Animator>().SetBool("Ko", true);
        yield return new WaitForSeconds(.59f);

        Destroy(gameObject); 
    }
}
