using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    /* ////////////////// */
    // Enemy Characteristics
    /* ////////////////// */
    [Header("Health & Mana")]
    [SerializeField]
    private int _maxHealth;

    [Header("Attack interaction")]
    [SerializeField]
    private int _strength;
    [SerializeField]
    private float _thrust;
    [SerializeField]
    private float _knockBackTime;
    [SerializeField]
    private bool _attackTypeMagic;
    [SerializeField]
    private bool _attackTypePhysic;

    /* Enemy State machine */
    public enum EnemyState {
        idle,
        move,
        knockBack,
        ko
    }

    /* Actual situation */
    private EnemyState _actualState;
    private int _actualHealth;

    private void Start()
    {
        _actualHealth    = _maxHealth;
        _actualState     = EnemyState.idle;
    }

    /* ************************************************ */
    /* Update functions */
    /* ************************************************ */  
    private int _UpdateForLimits(int variable, int limit)
    {
        if (variable > limit)
            return limit;

        if (variable < 0)
            return 0;

        return variable;
    }

    private EnemyState _GetState(string state)
    {
        switch (state)
        {
            case "idle":
                return EnemyState.idle;
            case "move":
                return EnemyState.move;
            case "knockBack":
                return EnemyState.knockBack;
            case "ko":
                return EnemyState.ko;
            default:
                return EnemyState.idle;
        }
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */

    /* if value < 0 => damage else heal */
    public int ActualHealth { 
        get { return _actualHealth; } 
        set {
            _actualHealth += value;
            _actualHealth = _UpdateForLimits(_actualHealth, _maxHealth); 
            
            if (_IsDead())
                Destroy(gameObject);
        }
    }

    public EnemyState ActualState {
        get { return _actualState; }
        set { _actualState = _GetState(value.ToString()); }
    }

    public int MaxHealth            { get; }
    public float KnockBackTime      { get; }
    public int Strength             { get; }
    public float Thrust             { get; }
    public bool AttackTypeMagic     { get; }
    public bool AttackTypePhysic    { get; }

    /* ************************************************ */
    /* Predicate */
    /* ************************************************ */
    private bool _IsDead(){
        return (_actualHealth <= 0);
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    private IEnumerator _AnimationDeath()
    {
        GetComponent<Animator>().SetBool("Ko", true);

        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(2).GetComponent<AudioManager>().CallAudio("Ko");

        yield return new WaitForSeconds(.59f);

        Destroy(gameObject); 
    }
}
