using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    [Header("Interaction parameters")]
    [SerializeField]
    private bool _isInvincible;
    [SerializeField]
    private float _invincibleTime = .5f;

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

        StartCoroutine(_KnockBackTimeCo(other.gameObject));
        StartCoroutine(_InvincibleTimeCo());
    }

    /* ************************************************ */
    /* Parameters functions */
    /* ************************************************ */
    /* KnockBack */

    /* If attack begin => enemy is knock */
    /* Else => enemy is not knock */
    // Vitesse mise à zero, seule la nouvelle force compte
    /* If attack begin => enemy becomes invincible */
    /* Else => enemy becomes vulnerable */

    private void KnockToggleParam(bool value)
    {
        _SetEnemyIsKnock(value);
        _SetVelocityToZero();
        _SetEnemyInvincible(value);
    }

    /* ************************************************ */
    /* Setters & toggle functions */
    /* ************************************************ */
    private void _SetEnemyIsKnock(bool value){
        _isKnock = value;
    }

    private void _SetVelocityToZero(){
        _rb2d.velocity = Vector2.zero;
    }

    //If enemy is invincible, then collider is disabled
    private void _SetEnemyInvincible(bool value)
    {
        _isInvincible     = value;
        _collider.enabled = !value;
    }

    /* ************************************************ */
    /* Apply force functions */
    /* ************************************************ */
    private Vector2 CalculateKnockBackDirection(Vector3 playerPosition){
        return (_parent.transform.position - playerPosition);
    }

    private void _ApplyThrustOnEnemy(Vector3 strengthDirection){
        _rb2d.AddForce(strengthDirection, ForceMode2D.Impulse);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* KnockBack time */
    private IEnumerator _KnockBackTimeCo(GameObject player)
    {
        Vector2 directionKnock  = CalculateKnockBackDirection(player.transform.position);
        Action playerAction = player.transform.parent.GetComponent<Action>();

        KnockToggleParam(true);

        _ApplyThrustOnEnemy(directionKnock * playerAction.GetThrust());        
        _CallHurt();

        yield return new WaitForSeconds(playerAction.GetKnockBackTime());

        _parent.GetComponent<EnemyTest>().ActualHealth = (playerAction.GetStrength() * -1);
    }

    /* Invincible time */
    private IEnumerator _InvincibleTimeCo()
    {
        float time         = .0f;
        Color regularColor = _sprite.color;

        while(time < _invincibleTime)
        {
            _sprite.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(0.02f);

            _sprite.color = regularColor;
            yield return new WaitForSeconds(0.02f);

            time += 0.2f;
        }

        _sprite.color = regularColor;

        KnockToggleParam(false);
    }

    /* ************************************************ */
    /* Audio */
    /* ************************************************ */
    /* Hurt */
    private void _CallHurt(){
        _parent.transform.GetChild(2).GetComponent<AudioManager>().CallAudio("hurt");
    }
}
