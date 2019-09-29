using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy parameters")]
    public int health;
    public string enemyName;

    public void ChangeHealth(int value){
        health += value;
        IsDead();
    }

    public void IsDead()
    {
        if (health <= 0)
            StartCoroutine(AnimationDeath());
    }

    private IEnumerator AnimationDeath()
    {
        GetComponent<Animator>().SetBool("Ko", true);
        yield return new WaitForSeconds(.59f);

        Destroy(gameObject); 
    }
}