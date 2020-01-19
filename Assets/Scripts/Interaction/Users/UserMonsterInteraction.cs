using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMonsterInteraction : MonoBehaviour, IUserMonsterInteraction
{
    private bool _isKnock;
    private bool _sensibleKnockBack;

    private bool _isInvincible;
    private bool _alwaysInvincible;
    private float _invincibleTime;

    private GameObject _parent;
    private BoxCollider2D _collider;

    public UserMonsterInteraction(bool sensibleKnockBack, bool alwaysInvincible, float invincibleTime)
    {
        _parent   = transform.parent.gameObject;
        _collider = GetComponent<BoxCollider2D>();

        _sensibleKnockBack  = sensibleKnockBack;
        _invincibleTime     = invincibleTime;
        _alwaysInvincible   = alwaysInvincible;

        _isKnock        = false;
        _isInvincible   = false;
    }

    public void InteractionWithAttack(Collider2D collider)
    {
        if (!collider.CompareTag("PlayerAttack"))
            return;

        if (_isKnock || _isInvincible)
            return;

        GameObject opponent = collider.gameObject;

        if (_sensibleKnockBack)
        {
            Vector2 directionKnock  = CalculateKnockBackDirection(opponent.transform.position);
            float thrust            = opponent.transform.parent.GetComponent<Action>().GetThrust();
            float knockBackTime     = opponent.transform.parent.GetComponent<Action>().GetKnockBackTime();

            new ConsequenceKnockBack().CallKnockBack(gameObject, directionKnock, thrust, knockBackTime);
        }

        if (!_alwaysInvincible)
        {
            ConsequenceDamage csqObject = new ConsequenceDamage();
            csqObject.DamageOnObject(_parent.GetComponent<Enemy>(), opponent.transform.parent.GetComponent<Action>().GetStrength());
            csqObject.CallInvincibleTime(gameObject, _parent.GetComponent<SpriteRenderer>(), _invincibleTime);
        }
    }

    /* ************************************************ */
    /* Knock update params */
    /* ************************************************ */
    private void KnockToggleParam(bool value)
    {
        _SetObjectIsKnock(value);
        _SetVelocityToZero();
        _SetObjectInvincible(value);
    }

    private void _SetObjectIsKnock(bool value){
        _isKnock = value;
    }

    private void _SetVelocityToZero(){
        _parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    //If object is invincible, then collider is disabled
    private void _SetObjectInvincible(bool value)
    {
        _isInvincible     = value;
        _collider.enabled = !value;
    }

    /* ************************************************ */
    /* Apply force functions */
    /* ************************************************ */
    /*private Vector2 CalculateKnockBackDirection(Vector3 opponentPosition){
        return (_parent.transform.position - opponentPosition);
    }

    private void _ApplyThrustOnObject(Vector3 strengthDirection){
        _parent.GetComponent<Rigidbody2D>().AddForce(strengthDirection, ForceMode2D.Impulse);
    }*/

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    /* KnockBack time */
    /*private IEnumerator _KnockBackTimeCo(Vector2 position, float thrust, float knockBackTime)
    {
        Vector2 directionKnock  = CalculateKnockBackDirection(position);

        KnockToggleParam(true);

        _ApplyThrustOnObject(directionKnock * thrust);        
        _CallHurt();

        yield return new WaitForSeconds(knockBackTime);
    }*/

    /* Invincible time */
    /*private IEnumerator _InvincibleTimeCo(SpriteRenderer sprite)
    {
        float time         = .0f;
        Color regularColor = sprite.color;

        while(time < _invincibleTime)
        {
            sprite.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(0.02f);

            sprite.color = regularColor;
            yield return new WaitForSeconds(0.02f);

            time += 0.2f;
        }

        sprite.color = regularColor;

        KnockToggleParam(false);
    }*/

    /* ************************************************ */
    /* Audio */
    /* ************************************************ */
    /* Hurt */
    private void _CallHurt(){
        _parent.transform.GetChild(2).GetComponent<AudioManager>().CallAudio("hurt");
    }
}
