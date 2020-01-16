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
    [SerializeField]
    private int _maxMana;

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
    [SerializeField]
    private int _actualHealth;
    private int _actualMana;

    void Start()
    {
        _actualHealth    = _maxHealth;
        _actualMana      = _maxMana;
        _actualState     = EnemyState.idle;
    }

    /* ************************************************ */
    /* Public functions */
    /* ************************************************ */  
    public void DamageHealth(int value)
    {
        _actualHealth -= value;
        if (_IsDead())
            _AnimationDeath();
    }
  
    public void Healhealth(int value)
    {
        _actualHealth += value;
        _UpdateForLimits(ref _actualHealth, _maxHealth);
    }

    public void DamageMana(int value){
        _actualMana -= value;
        _UpdateForLimits(ref _actualMana, _maxMana);
    }

    public void HealMana(int value)
    {
        _actualMana += value;
        _UpdateForLimits(ref _actualMana, _maxMana);
    }
    
    private void _UpdateForLimits(ref int variable, int limit)
    {
        if (variable > limit)
            variable = limit;

        if (variable < 0)
            variable = 0;
    }

    /* ************************************************ */
    /* Setter */
    /* ************************************************ */
    public void SetState(string state)
    {
        switch (state)
        {
            case "idle":
                _actualState = EnemyState.idle;
                break;
            case "move":
                _actualState = EnemyState.move;
                break;
            case "knockBack":
                _actualState = EnemyState.knockBack;
                break;
            case "ko":
                _actualState = EnemyState.ko;
                break;
            default:
                _actualState = EnemyState.idle;
                break;
        }
    }

    /* ************************************************ */
    /* Getter & Setter */
    /* ************************************************ */
    /* if value < 0 => damage else heal */
    public int ActualHealth { get; }
    public int ActualMana { get; }
    public int MaxHealth { get; }
    public int MaxMana { get; }
    public EnemyState ActualState { get; }
    public float KnockBackTime { get; }
    public int Strength { get; }
    public float Thrust { get; }
    public bool AttackTypeMagic { get; }
    public bool AttackTypePhysic { get; }

    /* ************************************************ */
    /* Question */
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
