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

    public void ChangeHealth(int value)
    {
        _health += value;
        IsDead();
    }

    public void ChangeMana(int value){
        _mana += value;
    }

    public void IsDead()
    {
        if (_health <= 0)
            StartCoroutine(AnimationDeath());
    }

    private IEnumerator AnimationDeath()
    {
        GetComponent<Animator>().SetBool("Ko", true);
        yield return new WaitForSeconds(.59f);

        Destroy(gameObject); 
    }
}
