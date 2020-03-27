using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    /* Parent components */
    private GameObject      _parent;
    private Rigidbody2D     _rb2d;
    private SpriteRenderer  _sprite;
    private EnemyTest       _enemyScript;

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

        _enemyScript    = _parent.GetComponent<EnemyTest>();

        _collider       = GetComponent<BoxCollider2D>();
        IsKnock         = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("PlayerAttack"))
            return;

        if (IsKnock || _enemyScript.IsInvincible)
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

    private void _KnockToggleParam(bool value)
    {
        IsKnock = value;
        _SetVelocityToZero();
        _SetEnemyInvincible(value);
    }

    /* ************************************************ */
    /* Setters & toggle functions */
    /* ************************************************ */
    public bool IsKnock {
        get { return _isKnock; }
        set { _isKnock = value; }
    }

    private void _SetVelocityToZero(){
        _rb2d.velocity = Vector2.zero;
    }

    //If enemy is invincible, then interaction collider is disabled
    private void _SetEnemyInvincible(bool value){
        _enemyScript.IsInvincible   = value;
        _collider.enabled           = !value;
    }

    /* ************************************************ */
    /* Apply force functions */
    /* ************************************************ */
    private Vector2 _CalculateKnockBackDirection(Vector3 playerPosition){
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
        Vector2 directionKnock  = _CalculateKnockBackDirection(player.transform.position);
        Attack playerAttack     = player.transform.parent.GetComponent<Attack>();

        _KnockToggleParam(true);

        _ApplyThrustOnEnemy(directionKnock * playerAttack.Thrust);        
        _CallHurt();

        yield return new WaitForSeconds(playerAttack.KnockBackTime);

        _enemyScript.ActualHealth = (playerAttack.GetStrength() * -1);
    }

    /* Invincible time */
    private IEnumerator _InvincibleTimeCo()
    {
        float time         = .0f;
        Color regularColor = _sprite.color;

        while(time < _enemyScript.InvincibleTime)
        {
            _sprite.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(0.02f);

            _sprite.color = regularColor;
            yield return new WaitForSeconds(0.02f);

            time += 0.2f;
        }

        _sprite.color = regularColor;

        _KnockToggleParam(false);
    }

    /* ************************************************ */
    /* Audio */
    /* ************************************************ */
    /* Hurt */
    private void _CallHurt(){
        _parent.transform.GetChild(2).GetComponent<AudioManager>().CallAudio("hurt");
    }
}
