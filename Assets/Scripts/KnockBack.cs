using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField]
    private float thrust = 0.2f;

    [SerializeField]
    private int _damagePower = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if(enemy != null)
        {
            Vector3 knockBackDir = (enemy.GetPosition() - transform.position).normalized;
            enemy.DamageKnockBack(knockBackDir, thrust, _damagePower);
        }  
    }
}
