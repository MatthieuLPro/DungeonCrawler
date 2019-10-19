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
            GameObject enemy = other.transform.parent.gameObject;
            Rigidbody2D enemyRb2d = enemy.GetComponent<Rigidbody2D>();

            if (enemy == null || enemyRb2d == null || _coIsRunning)
                return;

            if(enemy.GetComponent<Enemy>().isInvincible)
                return;

            _coIsRunning = true;
            Vector2 difference = enemy.transform.position - transform.position;
            difference = difference.normalized * _thrust;
            ChangeEnemyState(enemy);
            enemy.GetComponent<Enemy>().ChangeHealth(-1);
            enemyRb2d.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine((KnockCo(enemy)));
        }
        else if (other.CompareTag("GlobeSwitch")){
            StartCoroutine((SwitchCo(other)));
        }
    }

    private IEnumerator KnockCo(GameObject enemy)
    {
        //StartCoroutine(enemy.GetComponent<EnemyDirection>().FlashCo(_knockTime));
        yield return new WaitForSeconds(_knockTime);

        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ChangeEnemyState(enemy);
        _isColliding = false;
        _coIsRunning = false;
        enemy.GetComponent<Enemy>().ChangeHealth(_damage);
    }

    private IEnumerator SwitchCo(Collider2D globe)
    {
        globe.GetComponent<SwitchGlobe>().ToggleSwitchBlocks();

        yield return new WaitForSeconds(0.06f);
        _isColliding = false;
    }

    private void ChangeEnemyState(GameObject enemy)
    {
        GameObject movement     = enemy.transform.GetChild(0).gameObject;
        GameObject interaction  = enemy.transform.GetChild(1).gameObject;
        if (movement.GetComponent<EnemyDirection>().enabled == true)
        {
            movement.GetComponent<EnemyMovement>().currentState = EnemyState.knockBack;

            movement.GetComponent<EnemyDirection>().enabled = false;
            interaction.GetComponent<EnemyInteraction>().enabled = false;
            movement.GetComponent<EnemyMovement>().enabled = false;

            enemy.GetComponent<Animator>().SetBool("Moving", false);
        }
        else
        {
            movement.GetComponent<EnemyDirection>().enabled = true;
            interaction.GetComponent<EnemyInteraction>().enabled = true;
            movement.GetComponent<EnemyMovement>().enabled = true;

            movement.GetComponent<EnemyMovement>().currentState = EnemyState.idle;
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
