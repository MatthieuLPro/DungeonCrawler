using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* State machine */
public enum EnemyStateBis {
    idle,
    move,
    knockBack,
    ko
};

public interface IEnemy
{
    /* Functions */
    EnemyStateBis GetState(string state);

    /* Getter & Setter */
    EnemyStateBis ActualState   { get; set; }
    bool AttackTypeMagic        { get; }
    bool AttackTypePhysic       { get; }
    float KnockBackTime         { get; }
    int Strength                { get; }
    float Thrust                { get; }
}
