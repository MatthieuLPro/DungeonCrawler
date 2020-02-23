﻿using System.Collections;
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
    [SerializeField]
    private int _maxHealth;

    [Header("Enemy component")]
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

    private void Start()
    {
        _enemyComponent         = new Enemy(Strength, Thrust, KnockBackTime, AttackTypeMagic, AttackTypePhysic);
        _killableComponent      = new Killable();
        _damageableComponent    = new Damageable(MaxHealth);
    }

    /* ************************************************ */
    /* Enemy component */
    /* ************************************************ */
    public EnemyStateBis GetState(string state){
        return _enemyComponent.GetState(state);
    }

    public EnemyStateBis ActualState    { get; set; }
    public int MaxHealth {
        get { return _maxHealth; }
    }
    public float KnockBackTime {
        get { return _knockBackTime; }
    }
    public int Strength { 
        get { return _strength; }
    }
    public float Thrust { 
        get { return _thrust; }
    }
    public bool AttackTypePhysic {
        get { return _attackTypePhysic; }
    }
    public bool AttackTypeMagic {
        get { return _attackTypeMagic;}
    }

    /* ************************************************ */
    /* Damageable component */
    /* ************************************************ */
    public int ActualHealth { 
        get { return _damageableComponent.ActualHealth; } 
        set {
            _damageableComponent.ActualHealth = value;
            if(IsDead(_damageableComponent.ActualHealth))
                AnimationDead(gameObject);
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
