using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionFront : MonoBehaviour
{
    /* Parent components */
    private GameObject      _parent;
    private Movement        _movement;
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
        _anime          = _parent.GetComponent<Animator>();
        _rb2d           = _parent.GetComponent<Rigidbody2D>();
        _sprite         = _parent.GetComponent<SpriteRenderer>();

        _collider       = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("ObjectOpen")) {
            StartCoroutine(_ObjectConnectionCo(other.gameObject));
            return;
        }
        ObjectOpen = null;
    }

    void OnTriggerExit2D(Collider2D other) {
        ObjectOpen = null;
    }

    IEnumerator _ObjectConnectionCo(GameObject gameObject) {
        yield return null;
        ObjectOpen = gameObject;
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    public GameObject ObjectOpen {
        get => _objectOpen;
        set => _objectOpen = value;
    }
}
