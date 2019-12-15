using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{    
    [Header("Action settings")]
    public float strength;
    public float knockBackTime;

    /* Parent components */
    private GameObject              _parent;
    private TestMovement            _movement;
    private TestInteractionFront    _interactionFront;
    private Animator                _anime;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Start()
    {
        _parent             = transform.parent.gameObject;
        _movement           = _parent.transform.Find("MovementTest").GetComponent<TestMovement>();
        _interactionFront   = _parent.transform.Find("InteractionTest").transform.Find("Front").GetComponent<TestInteractionFront>();
        _anime              = _parent.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Action Button
        if (InputManager.YButton())
        {
            /* Priority Order */
            /*
                => Throw object if have object
                => Carry object if object in front
                => Attack
            */
            if (transform.Find("Pot") || transform.Find("Bush"))
                ThrowCarryObject();
            if (_interactionFront.objectCarry != null)
                _interactionFront.InteractionWithObjectCarry();
            else if (_movement.currentState != TestObjectState.knock)
                StartCoroutine(MainAttack());

        }
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */
    /* Throw object */
    private void ThrowCarryObject()
    {

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
