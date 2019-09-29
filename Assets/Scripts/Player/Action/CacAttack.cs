using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacAttack : MonoBehaviour
{
    // _KnockTime has to be < MainAttack Time
    // damage if _damage is negative / heal if _damage is positive
    [Header("Attack parameters")]
    [SerializeField]
    private float _knockTime = 0.2f;
    [SerializeField]
    private float  _thrust = 2f;
    [SerializeField]
    private int  _damage = -1;

    private bool _isColliding = false;
    private bool _coIsRunning = false;

    // Need to check if isColliding & CoisRunning are usefull
    private void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("Time begin - OnTrigger: " + Time.time);
        if (_isColliding)
            return;

        _isColliding = true;
        if(ObjectIsDestructible(other.gameObject) == true)
        {
            other.GetComponent<DestructibleObject>().Smash();
            _isColliding = false;
        }
        else if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();

            if (enemy == null || _coIsRunning)
                return;

            _coIsRunning = true;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * _thrust;
            ChangeEnemyState(enemy);
            //ChangeEnemyLifeOrMana(enemy);
            enemy.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine((KnockCo(enemy)));
        }
        Debug.Log("Time end - OnTrigger: " + Time.time);
    }

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        Debug.Log("Time begin - KnockCo: " + Time.time);
        StartCoroutine(enemy.GetComponent<EnemyController>().FlashCo(_knockTime));
        yield return new WaitForSeconds(_knockTime);

        enemy.velocity = Vector2.zero;
        ChangeEnemyState(enemy);
        _isColliding = false;
        _coIsRunning = false;
        enemy.GetComponent<Enemy>().ChangeHealth(_damage);
        Debug.Log("Time end - KnockCo: " + Time.time);
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
        }
        else
        {
            enemy.GetComponent<EnemyController>().enabled = true;
            enemy.GetComponent<EnemyInteraction>().enabled = true;
            enemy.GetComponent<MovingObject>().enabled = true;

            enemy.GetComponent<MovingObject>().currentState = ObjectState.idle;
        }
    }

    private bool ObjectIsDestructible(GameObject objectToCheck)
    {
        if (objectToCheck.CompareTag("ObjectDestructible"))
            return true;
        
        foreach (Transform child in objectToCheck.transform)
        {
            if (child.CompareTag("ObjectDestructible"))
                return true;
        }
        return false;
    }
}