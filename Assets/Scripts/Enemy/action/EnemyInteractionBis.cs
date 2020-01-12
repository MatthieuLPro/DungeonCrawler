using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractionBis : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private int _typeDamage = 0;
    [SerializeField]
    private float _thrust       = 1f;    
    [SerializeField]
    private float _knockTime    = 0.3f;

    /*
        TypeDamage:
            0 => Life
            1 => Mana
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        Rigidbody2D player = other.GetComponent<Rigidbody2D>();
        if (player == null)
            return;

        if(GetComponent<BoxCollider2D>().enabled)
            EnemyHitPlayer(player);
        else
            transform.GetChild(0).transform.GetComponent<EnemyMovement>()._isWakeUpTransition = true;
    }

    
    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    // Enemy touches the player
    private void EnemyHitPlayer(Rigidbody2D player){
        Vector2 difference = player.transform.position - transform.position;

        ChangePlayerState(player);
        ChangePlayerLifeOrMana(player);

        player.AddForce(difference.normalized * _thrust, ForceMode2D.Impulse);
        StartCoroutine((KnockCo(player)));
    }

    /* ************************************************ */
    /* Change player state functions */
    /* ************************************************ */
    // Changer player animation
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

    // Changer player variables (life and mana) 
    private void ChangePlayerLifeOrMana(Rigidbody2D player)
    {
        if (_typeDamage == 0)
            player.GetComponent<Player>().LooseLife(_damage);
        else
            player.GetComponent<Player>().LooseMana(_damage);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    // Knock out player
    private IEnumerator KnockCo(Rigidbody2D player){
        StartCoroutine(player.GetComponent<PlayerController>().FlashCo(_knockTime));
        player.GetComponent<AudioManager>().CallAudio("hurt");
        yield return new WaitForSeconds(_knockTime);

        player.velocity = Vector2.zero;
        ChangePlayerState(player);
    }
}