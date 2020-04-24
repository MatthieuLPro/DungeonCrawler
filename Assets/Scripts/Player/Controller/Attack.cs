using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Attack : MonoBehaviour
{    
    [Header("Action settings")]
    public float thrust;
    public float knockBackTime;

    /* Parent components */
    [SerializeField]
    private GameObject              _parent;
    private string                  _player_name;
    [SerializeField]
    private Movement                _movement;
    private TestInteractionFront    _interactionFront;
    [SerializeField]
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
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */
    /* Action functions */
    public void ActionsList()
    {
        if (_movement.currentState != TestObjectState.knock)
            StartCoroutine(MainAttack());

        /* Priority Order */
        /*
            => Throw object if have object
            => Carry object if object in front
            => Attack
        */
        /*if (transform.Find("Pot") || transform.Find("Bush"))
            ThrowCarryObject();
        if (_interactionFront.objectCarry != null)
            _interactionFront.InteractionWithObjectCarry();
        else if (_movement.currentState != TestObjectState.knock)
            StartCoroutine(MainAttack());*/
    }

    /* Throw object */
    /*private void ThrowCarryObject()
    {

    }*/

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* Main Attack */
    private IEnumerator MainAttack()
    {
        _anime.SetBool("Attacking", true);

        _movement.MovementIsBlock = true;

        _parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _parent.transform.parent.Find("Audio").GetComponent<AudioManager>().CallAudio("attack");
        _parent.transform.parent.Find("Audio").GetComponent<AudioManager>().PlayAudio();

        yield return null;

        _anime.SetBool("Attacking", false);

        yield return new WaitForSeconds(_anime.GetCurrentAnimatorClipInfo(0)[0].clip.length - .3f);

        _movement.MovementIsBlock = false;
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    public float Thrust {
        get { return thrust; }
        set { thrust = value; }
    }

    public float KnockBackTime {
        get { return knockBackTime; }
        set { knockBackTime = value; }
    }

    public int GetStrength(){
        return _parent.transform.parent.GetComponent<Player>().Strength;
    }
}
