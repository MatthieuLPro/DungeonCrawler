using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionFront : MonoBehaviour
{
    /* Parent components */
    private GameObject      _parent;
    private Movement        _movement;
    //private TestAction      _action;
    private Animator        _anime;
    private Rigidbody2D     _rb2d;
    private SpriteRenderer  _sprite;

    /* Interaction components */
    private BoxCollider2D   _collider;

    private bool _isKnock;

    public GameObject objectCarry = null;
    public GameObject _objectOpen = null;

    /* ************************************************ */
    /* Main Functions */
    /* ************************************************ */
    void Start()
    {
        _parent         = transform.parent.transform.parent.gameObject;

        _movement       = _parent.transform.GetChild(0).GetComponent<Movement>();
        //_action         = _parent.transform.Find("ActionTest").GetComponent<TestAction>();
        _anime          = _parent.GetComponent<Animator>();
        _rb2d           = _parent.GetComponent<Rigidbody2D>();
        _sprite         = _parent.GetComponent<SpriteRenderer>();

        _collider       = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        //if (other.CompareTag("ObjectCarry")) {
        //    objectCarry = other.gameObject;
        //    return;
        //}
        //objectCarry = null;

        if (other.CompareTag("ObjectOpen")) {
            ObjectOpen = other.gameObject;
            return;
        }
        ObjectOpen = null;
    }

    void OnTriggerExit2D(Collider2D other) {
        ObjectOpen = null;
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */
    /* Tag: Object Carry */
    //public void InteractionWithObjectCarry()
    //{
    //    objectCarry.GetComponent<SpriteRenderer>().sprite = null;
    //    objectCarry.GetComponent<CarryObjectManager>().GenerateCollectible();
    //    objectCarry.GetComponent<CarryObjectManager>().CarryObject(_parent);
    //    objectCarry = null;
    //}

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    public GameObject ObjectOpen {
        get { return _objectOpen; }
        set { _objectOpen = value; }
    }
}
