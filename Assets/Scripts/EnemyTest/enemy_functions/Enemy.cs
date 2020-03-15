using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    private int _strength;
    private float _thrust;
    private float _knockBackTime;
    private bool _attackTypeMagic;
    private bool _attackTypePhysic;

    public Enemy(int strength, float thrust, float knockBackTime, bool attackTypeMagic, bool attackTypePhysic)
    {
        _strength           = strength;
        _thrust             = thrust;
        _knockBackTime      = knockBackTime;
        _attackTypeMagic    = attackTypeMagic;
        _attackTypePhysic   = attackTypePhysic;
    }

    private EnemyStateBis _actualState;

    public Enemy(){
        ActualState = GetState("idle");
    }

    public EnemyStateBis GetState(string state)
    {
        switch (state)
        {
            case "idle":
                return EnemyStateBis.idle;
            case "move":
                return EnemyStateBis.move;
            case "knockBack":
                return EnemyStateBis.knockBack;
            case "ko":
                return EnemyStateBis.ko;
            default:
                return EnemyStateBis.idle;
        }
    }

    public EnemyStateBis ActualState   { get; set; }
    public bool AttackTypeMagic        { get; }
    public bool AttackTypePhysic       { get; }
    public float KnockBackTime         { get; }
    public int Strength                { get; }
    public float Thrust                { get; }
}
