using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField]
    private float _knockTime    = 1f,
                  _thrust       = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Rigidbody2D player = other.GetComponent<Rigidbody2D>();
        if (player == null)
            return;
        
        Vector2 difference = player.transform.position - transform.position;
        difference = difference.normalized * _thrust;
        
        player.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine((KnockCo(player)));
    }

    private IEnumerator KnockCo(Rigidbody2D player)
    {
        yield return new WaitForSeconds(_knockTime);

        player.velocity = Vector2.zero;
    }
}
