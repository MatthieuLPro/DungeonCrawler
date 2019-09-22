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
        ChangePlayerState(player);
        player.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine((KnockCo(player)));
    }

    private IEnumerator KnockCo(Rigidbody2D player)
    {
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
}
