using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestInteraction : MonoBehaviour
{
    [Header("Interaction parameters")]
    [SerializeField]
    private bool _isInvincible;
    [SerializeField]
    private float invincibleTime = .5f;

    /* Parent components */
    private GameObject      _parent;
    private Rigidbody2D     _rb2d;
    private SpriteRenderer  _sprite;

    /* Interaction components */
    private BoxCollider2D   _collider;

    private bool _isKnock;

    /* ************************************************ */
    /* Main Functions */
    /* ************************************************ */
    void Start()
    {
        _parent         = transform.parent.gameObject;

        _rb2d           = _parent.GetComponent<Rigidbody2D>();
        _sprite         = _parent.GetComponent<SpriteRenderer>();

        _collider       = GetComponent<BoxCollider2D>();
        _isKnock        = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerAttack"))
            return;

        if (_isKnock || _isInvincible)
            return;

        StartCoroutine(KnockCo(other.gameObject));
        StartCoroutine(InvincibleCo(other.gameObject));
    }

    /* ************************************************ */
    /* Parameters functions */
    /* ************************************************ */
    /* KnockBack */
    private void KnockToggleParam()
    {
        // Rentrer en contact avec un enemy
        _isKnock                = !_isKnock;

        // Vitesse mise à zero, seule la nouvelle force compte
        _rb2d.velocity          = Vector2.zero;

        if (_rb2d.bodyType == RigidbodyType2D.Kinematic)
            _rb2d.bodyType = RigidbodyType2D.Dynamic;
        else
            _rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    private void InvincibleParam()
    {
        _isInvincible     = !_isInvincible;
        _collider.enabled = !_collider.enabled;
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* KnockBack */
    private IEnumerator KnockCo(GameObject player)
    {
        Vector2 directionKnock  = _parent.transform.position - player.transform.position;
        float strength          = player.transform.parent.GetComponent<TestAction>().strength;
        float knockTime         = player.transform.parent.GetComponent<TestAction>().knockBackTime;
        
        KnockToggleParam();
        
        // Application de la nouvelle force
        _rb2d.AddForce(directionKnock * strength, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockTime);

        KnockToggleParam();
    }

    /* Invincible (post knockback) */
    private IEnumerator InvincibleCo(GameObject player)
    {
        float knockTime = player.transform.parent.GetComponent<TestAction>().knockBackTime;

        // Impossible de rentrer a nouveau en contact avec un enemy
        InvincibleParam();

        yield return new WaitForSeconds(knockTime);

        float time         = .0f;
        Color regularColor = _sprite.color;

        while(time < invincibleTime)
        {
            _sprite.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(0.02f);

            _sprite.color = regularColor;
            yield return new WaitForSeconds(0.02f);

            time += 0.2f;
        }

        _sprite.color = regularColor;

        InvincibleParam();
    }
}
