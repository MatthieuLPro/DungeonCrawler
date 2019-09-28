using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [Header("Enemy Attack parameters")]
    [SerializeField]
    private float _knockTime    = 0.3f;
    [SerializeField]
    private float _thrust       = 1f;    
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    // 0 => Deal life damage
    // 1 => Deal mana damage
    private int _typeDamage = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Rigidbody2D player = other.GetComponent<Rigidbody2D>();
        if (player == null)
            return;
        
        Vector2 difference = player.transform.position - transform.position;
        difference = difference.normalized * _thrust;
        ChangePlayerState(player);
        ChangePlayerLifeOrMana(player);
        player.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine((KnockCo(player)));
    }

    private IEnumerator KnockCo(Rigidbody2D player)
    {
        StartCoroutine(player.GetComponent<PlayerController>().FlashCo());
        yield return new WaitForSeconds(_knockTime);

        player.velocity = Vector2.zero;
        ChangePlayerState(player);
    }

    private void ChangePlayerState(Rigidbody2D player)
    {
        if (player.GetComponent<PlayerController>().enabled == true)
        {
            player.GetComponent<MovingObject>().currentState = ObjectState.knockBack;

            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<MovingObject>().enabled = false;

            player.GetComponent<Animator>().SetBool("Moving", false);
            player.GetComponent<Animator>().SetBool("KnockBacking", true);
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<MovingObject>().enabled = true;

            player.GetComponent<Animator>().SetBool("KnockBacking", false);
            player.GetComponent<MovingObject>().currentState = ObjectState.idle;
        }
    }

    private void ChangePlayerLifeOrMana(Rigidbody2D player)
    {
        if (_typeDamage == 0)
            player.GetComponent<Player>().LooseLife(_damage);
        else
            player.GetComponent<Player>().LooseMana(_damage);
    }
}