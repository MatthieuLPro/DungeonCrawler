using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{    
    [Header("Action settings")]
    public float strength;
    public float knockBackTime;

    /* Parent components */
    private GameObject      _parent;
    private TestMovement    _movement;
    private Animator        _anime;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Start()
    {
        _parent         = transform.parent.gameObject;
        _movement       = _parent.transform.Find("MovementTest").GetComponent<TestMovement>();
        _anime          = _parent.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (InputManager.YButton() && _movement.currentState != TestObjectState.knock)
            StartCoroutine(MainAttack());
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* Main Attack */
    private IEnumerator MainAttack()
    {
        _anime.SetBool("Attacking", true);

        _movement.blockMovement = true;

        _parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return null;

        _anime.SetBool("Attacking", false);

        yield return new WaitForSeconds(_anime.GetCurrentAnimatorClipInfo(0)[0].clip.length - .3f);

        _movement.blockMovement = false;
    }
}
