using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractionGlobal : MonoBehaviour
{
    [Header("Interaction parameters")]
    [SerializeField]
    private bool _isInvincible;
    [SerializeField]
    private float _invincibleTime = 5;

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
        IsKnock        = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.transform.parent.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Exit 1");
            InteractionWithEnemy(other.transform.parent.gameObject);
            return;
        }*/

        if (other.CompareTag("Enemy"))
        {
            InteractionWithEnemy(other.gameObject);
            if (other.gameObject.transform.gameObject.GetComponent<EnemyTest>().DestroyOnTouch)
                Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("PlayerAttack"))
        {
            InteractionWithPlayer(other.gameObject.transform.gameObject);
            return;
        }

        if (other.CompareTag("PlayerPush"))
        {
            InteractionWithObject(other.gameObject.transform.gameObject);
            Destroy(other.gameObject);
            return;
        }
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */
    /* Tag: Enemy */
    private void InteractionWithEnemy(GameObject enemy)
    {
        if (IsKnock || IsInvincible)
            return;

        _ApplyDamageFromEnemy(enemy.GetComponent<EnemyTest>(), _parent.transform.parent.GetComponent<Player>());
        StartCoroutine(_KnockBackTimeEnemyCo(enemy));
        StartCoroutine(_InvincibleTimeCo());
    }

    /* Tag: PlayerAttack */
    private void InteractionWithPlayer(GameObject player)
    {
        if (IsKnock || IsInvincible)
            return;

        //_ApplyDamageFromEnemy(enemy.GetComponent<EnemyTest>(), _parent.transform.parent.GetComponent<Player>());
        StartCoroutine(_KnockBackTimePlayerCo(player));
        StartCoroutine(_InvincibleTimeCo());
    }

    /* Tag: PlayerPush */
    private void InteractionWithObject(GameObject pushObject)
    {
        if (IsKnock || IsInvincible)
            return;

        StartCoroutine(_KnockBackTimeObjectCo(pushObject));
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
        _SetPlayerIsKnock(value);
        _SetVelocityToZero();
        if (_anime.GetBool("Specialing") && _parent.transform.parent.GetComponent<Player>().CharacterType == "LinkPurple")
            return;
        
        SetPlayerInvincible(value);
    }

    private void _BlockMovement(bool value)
    {
        _SetBlockPlayerMovement(value);
        _SetPlayerStateMachine();
        AnimationKnockBack();
    }

    /* ************************************************ */
    /* Setters & toggle functions */
    /* ************************************************ */
    private void _SetPlayerIsKnock(bool value){
        IsKnock = value;
    }

    private void _SetVelocityToZero(){
        _rb2d.velocity = Vector2.zero;
    }

    //If enemy is invincible, then collider is disabled
    public void SetPlayerInvincible(bool value)
    {
        IsInvincible      = value;
        _collider.enabled = !value;
    }

    private void _SetPlayerStateMachine()
    {
        if (_movement.currentState == TestObjectState.knock)
            _movement.currentState = TestObjectState.idle;
        else
            _movement.currentState = TestObjectState.knock;
    }

    private void _SetBlockPlayerMovement(bool value){
        _movement.MovementIsBlock = value;
    }

    /* ************************************************ */
    /* Apply damages on player functions */
    /* ************************************************ */
    private void _ApplyDamageFromEnemy(EnemyTest enemy, Player player)
    {
        if (enemy.AttackTypePhysic) player.LooseLife(enemy.Strength);
        if (enemy.AttackTypeMagic) player.LooseMana(enemy.Strength);
    }

    private void _PhysicalDamageFromEnemy(Player player, int strength){
        player.LooseLife(strength);
    }

    private void _MagicalDamageFromEnemy(Player player, int strength){
        player.LooseMana(strength);
    }

    /* ************************************************ */
    /* Apply force functions */
    /* ************************************************ */
    private Vector2 CalculateKnockBackDirection(Vector3 opposantPosition){
        return (_parent.transform.position - opposantPosition);
    }

    private void _ApplyThrustOnPlayer(Vector3 strengthDirection){
        _rb2d.AddForce(strengthDirection, ForceMode2D.Impulse);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* KnockBack */
    private IEnumerator _KnockBackTimeEnemyCo(GameObject enemy)
    {
        // Get Direction of knockback
        Vector2 directionKnock  = CalculateKnockBackDirection(enemy.transform.position);
        EnemyTest enemyTest     = enemy.GetComponent<EnemyTest>();
        
        KnockToggleParam(true);
        _BlockMovement(true);
        
        // Application de la nouvelle force
        _ApplyThrustOnPlayer(directionKnock * enemyTest.Thrust);
        _CallHurt();
        yield return new WaitForSeconds(enemyTest.KnockBackTime);
        _BlockMovement(false);
    }

    private IEnumerator _KnockBackTimePlayerCo(GameObject player)
    {
        // Get Direction of knockback
        Vector2 directionKnock   = CalculateKnockBackDirection(player.transform.position);
        Attack playerAttack      = player.transform.parent.GetComponent<Attack>();
        
        KnockToggleParam(true);
        _BlockMovement(true);
        
        // Application de la nouvelle force
        _ApplyThrustOnPlayer(directionKnock * playerAttack.Thrust);
        _CallHurt();
        yield return new WaitForSeconds(playerAttack.KnockBackTime);
        _BlockMovement(false);
    }

    private IEnumerator _KnockBackTimeObjectCo(GameObject pushObject)
    {
        // Get Direction of knockback
        Vector2 directionKnock          = CalculateKnockBackDirection(pushObject.transform.position);
        ConsumableBanana bananaAttack   = pushObject.GetComponent<ConsumableBanana>();
        
        KnockToggleParam(true);
        _BlockMovement(true);
        
        // Application de la nouvelle force
        _ApplyThrustOnPlayer(directionKnock * bananaAttack.Thrust);
        _CallHurt();
        yield return new WaitForSeconds(bananaAttack.KnockBackTime);
        _BlockMovement(false);
    }

    private IEnumerator _InvincibleTimeCo()
    {
        float time         = .0f;
        Color regularColor = _sprite.color;

        while(time < InvincibleTime)
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
    /* Animations */
    /* ************************************************ */
    /* KnockBack */
    private void AnimationKnockBack()
    {
        if (_anime.GetBool("Moving") == true)
            _anime.SetBool("Moving", false);

        _anime.SetBool("KnockBacking", !_anime.GetBool("KnockBacking"));
    }

    /* ************************************************ */
    /* Audio */
    /* ************************************************ */
    /* Hurt */
    private void _CallHurt(){
        _parent.transform.parent.Find("Audio").GetComponent<AudioManager>().CallAudio("hurt");
        _parent.transform.parent.Find("Audio").GetComponent<AudioManager>().PlayAudio();
    }


    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */

    public bool IsInvincible {
        get => _isInvincible;
        set => _isInvincible = value;
    }

    public float InvincibleTime {
        get => _invincibleTime;
        set => _invincibleTime = value;
    }

    public bool IsKnock {
        get => _isKnock;
        set => _isKnock = value;
    }
}
