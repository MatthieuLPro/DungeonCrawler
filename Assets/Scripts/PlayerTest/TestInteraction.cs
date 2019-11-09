using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : MonoBehaviour
{
    /* Parent components */
    private GameObject      _parent;
    private TestMovement    _movement;
    private Animator        _anime;
    private Rigidbody2D     _rb2d;

    private bool _isKnock;

    void Start()
    {
        _parent         = transform.parent.gameObject;
        _movement       = _parent.transform.Find("MovementTest").GetComponent<TestMovement>();
        _anime          = _parent.GetComponent<Animator>();
        _rb2d           = _parent.GetComponent<Rigidbody2D>();
        _isKnock        = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        if (_isKnock)
            return;

        _movement.blockMovement = true;

        GameObject enemy        = other.gameObject;
        Vector2 directionKnock  = enemy.transform.position - _parent.transform.position;

        _rb2d.AddForce(directionKnock * enemy.GetComponent<EnemyTest>().strength, ForceMode2D.Impulse);
        StartCoroutine(KnockCo());
    }

    private IEnumerator KnockCo()
    {
        _isKnock                = true;
        _movement.currentState  = TestObjectState.knock;

        _anime.SetBool("Moving", false);
        _anime.SetBool("KnockBacking", true);

        yield return new WaitForSeconds(.5f);

        _rb2d.velocity              = Vector2.zero;
        _isKnock                    = false;
        _movement.blockMovement     = false;        
        _movement.currentState      = TestObjectState.idle;

        _anime.SetBool("KnockBacking", false);
    }   
}
