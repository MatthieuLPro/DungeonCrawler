using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{    
    [Header("Action settings")]
    public float strength;
    public float knockBackTime;

    /* Parent components */
    private GameObject              _parent;
    private string                  _player_name;
    private Movement                _movement;
    private TestInteractionFront    _interactionFront;
    private Animator                _anime;

    /* ************************************************ */
    /* Main functions */
    /* ************************************************ */
    void Start()
    {
        _parent             = transform.parent.gameObject;
        _player_name        = _parent.transform.parent.name; 
        _movement           = _parent.transform.Find("Movement").GetComponent<Movement>();
        _interactionFront   = _parent.transform.Find("Interaction").transform.Find("Front").GetComponent<TestInteractionFront>();
        _anime              = _parent.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Action Button
        if (_player_name == "Player_1")
        {
            if(InputManagerPlayer1.BButton())
                ActionsList();
        }
        else if (_player_name == "Player_2")
        {
            if(InputManagerPlayer2.BButton())
                ActionsList();
        }
        else if (_player_name == "Player_3")
        {
            if(InputManagerPlayer3.BButton())
                ActionsList();
        }
        else
        {
            if(InputManagerPlayer4.BButton())
                ActionsList();
        }
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */
    /* Action functions */
    private void ActionsList()
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
        _parent.transform.parent.Find("Audio").GetComponent<AudioManager>().CallAudio("attack");

        yield return null;

        _anime.SetBool("Attacking", false);

        yield return new WaitForSeconds(_anime.GetCurrentAnimatorClipInfo(0)[0].clip.length - .3f);

        _movement.blockMovement = false;
    }
}
