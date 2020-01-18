using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour, IEnemy, IKillable, IDamageable
{
    /* Enemy components */
    private Enemy _enemyComponent;
    private Killable _killableComponent;
    private Damageable _damageableComponent;

    /* Initial enemis values */
    [Header("Damageable component")]
    public int maxHealth;

    [Header("Enemy component")]
    public int strength;
    public float thrust;
    public float knockBackTime;
    public bool attackTypeMagic;
    public bool attackTypePhysic;

    private void Start()
    {
        _enemyComponent         = new Enemy(strength, thrust, knockBackTime, attackTypeMagic, attackTypePhysic);
        _killableComponent      = new Killable();
        _damageableComponent    = new Damageable(maxHealth);
    }

    /* ************************************************ */
    /* Enemy component */
    /* ************************************************ */
    public EnemyStateBis GetState(string state){
        return _enemyComponent.GetState(state);
    }

    public EnemyStateBis ActualState    { get; set; }
    public int MaxHealth                { get; }
    public float KnockBackTime          { get; }
    public int Strength                 { get; }
    public float Thrust                 { get; }
    public bool AttackTypeMagic         { get; }
    public bool AttackTypePhysic        { get; }

    /* ************************************************ */
    /* Damageable component */
    /* ************************************************ */
    public int ActualHealth { 
        get { return _damageableComponent.ActualHealth; } 
        set {
            _damageableComponent.ActualHealth = value;
            if(_killableComponent.IsDead(_damageableComponent.ActualHealth))
                _killableComponent.AnimationDead(gameObject);
        }
    }

    public int GetNewValue(int actualHealth, int value){
        return (_damageableComponent.GetNewValue(actualHealth, value));
    }

    public int GetValueFromLimits(int variable, int limit){
        return (_damageableComponent.GetValueFromLimits(variable, limit));
    }

    /* ************************************************ */
    /* Killable component */
    /* ************************************************ */
    public bool IsDead(int actualHealth){
        return (_killableComponent.IsDead(actualHealth));
    }

    public void AnimationDead(GameObject enemyObject){
        _killableComponent.AnimationDead(enemyObject);
    }
}
