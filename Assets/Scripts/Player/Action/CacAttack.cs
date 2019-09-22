using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacAttack : MonoBehaviour
{
    // _KnockTime has to be < MainAttack Time
    [SerializeField]
    private float _knockTime    = 0.25f,
                  _thrust       = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("ItemDestructible"))
            other.GetComponent<DestructibleItem>().Smash();
        else if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();

            if (enemy == null)
                return;

            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * _thrust;
            ChangeEnemyState(enemy);
            //ChangeEnemyLifeOrMana(enemy);
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine((KnockCo(enemy)));
        }
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        yield return new WaitForSeconds(_knockTime);

        enemy.velocity = Vector2.zero;
        ChangeEnemyState(enemy);
    }

    private void ChangeEnemyState(Rigidbody2D enemy)
    {
        if (enemy.GetComponent<EnemyController>().enabled == true)
        {
            enemy.GetComponent<MovingObject>().currentState = ObjectState.knockBack;

            enemy.GetComponent<EnemyController>().enabled = false;
            enemy.GetComponent<EnemyInteraction>().enabled = false;
            enemy.GetComponent<MovingObject>().enabled = false;

            enemy.GetComponent<Animator>().SetBool("Moving", false);
            //enemy.GetComponent<Animator>().SetBool("KnockBacking", true);
        }
        else
        {
            enemy.GetComponent<EnemyController>().enabled = true;
            enemy.GetComponent<EnemyInteraction>().enabled = true;
            enemy.GetComponent<MovingObject>().enabled = true;

            //enemy.GetComponent<Animator>().SetBool("KnockBacking", false);
            enemy.GetComponent<MovingObject>().currentState = ObjectState.idle;
        }
    }
}