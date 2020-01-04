using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionGlobal : MonoBehaviour
{
    [Header("Interaction parameters")]
    [SerializeField]
    private bool _isInvincible;
    [SerializeField]
    private float invincibleTime = 5;

    /* Parent components */
    private GameObject      _parent;
    private Movement        _movement;
    private Animator        _anime;
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
        _parent         = transform.parent.transform.parent.gameObject;

        _movement       = _parent.transform.Find("Movement").GetComponent<Movement>();
        _anime          = _parent.GetComponent<Animator>();
        _rb2d           = _parent.GetComponent<Rigidbody2D>();
        _sprite         = _parent.GetComponent<SpriteRenderer>();

        _collider       = GetComponent<BoxCollider2D>();
        _isKnock        = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            InteractionWithEnemy(other.gameObject);
            return;
        }
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */
    /* Tag: Enemy */
    private void InteractionWithEnemy(GameObject enemy)
    {
        if (_isKnock || _isInvincible)
            return;

        // Need to adapt if loose life or mana (depend of enemy characteristics)
        // Need to adapt amount of life or mana to change (depend of enemy characteristics)
        DamageFromEnemyParam(enemy.GetComponent<EnemyTest>());
        StartCoroutine(KnockCo(enemy));
        StartCoroutine(InvincibleCo(enemy));
    }

    /* ************************************************ */
    /* Parameters functions */
    /* ************************************************ */
    /* KnockBack */
    private void KnockToggleParam()
    {
        // Rentrer en contact avec un enemy
        _isKnock                = !_isKnock;

        // Bouger pendant l'animation knockBack
        _movement.blockMovement = !_movement.blockMovement;

        // Vitesse mise à zero, seule la nouvelle force compte
        _rb2d.velocity          = Vector2.zero;
        
        // Nouvelle etat machine personnage
        if (_movement.currentState == TestObjectState.knock)
            _movement.currentState = TestObjectState.idle;
        else
            _movement.currentState = TestObjectState.knock;

        AnimationKnockBack();
    }

    private void InvincibleToggleParam()
    {
        _isInvincible     = !_isInvincible;
        _collider.enabled = !_collider.enabled;
    }

    private void DamageFromEnemyParam(EnemyTest enemyParam)
    {
        if (enemyParam.attackTypeMagic)
            _parent.transform.parent.GetComponent<Player>().LooseMana(1);
        if (enemyParam.attackTypePhysic)
            _parent.transform.parent.GetComponent<Player>().LooseLife(1);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* KnockBack */
    private IEnumerator KnockCo(GameObject enemy)
    {
        // Get Direction of knockback
        Vector2 directionKnock  = _parent.transform.position - enemy.transform.position;
        
        KnockToggleParam();
        
        // Application de la nouvelle force
        _rb2d.AddForce(directionKnock * enemy.GetComponent<EnemyTest>().strength, ForceMode2D.Impulse);

        yield return new WaitForSeconds(enemy.GetComponent<EnemyTest>().knockBackTime);

        KnockToggleParam();
    }

    /* Invincible (post knockback) */
    private IEnumerator InvincibleCo(GameObject enemy)
    {
        // Impossible de rentrer a nouveau en contact avec un enemy
        InvincibleToggleParam();

        yield return new WaitForSeconds(enemy.GetComponent<EnemyTest>().knockBackTime);

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

        InvincibleToggleParam();
    }

    /* ************************************************ */
    /* Animations */
    /* ************************************************ */
    /* KnockBack */
    private void AnimationKnockBack()
    {
        if (_anime.GetBool("Moving") == true)
            _anime.SetBool("Moving", false);

        _anime.SetBool("KnockBacking", !_anime.GetBool("KnockBacking"));
    }
}
