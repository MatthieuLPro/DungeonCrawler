using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    bool IsDead(int actualHealth);
    void AnimationDead(GameObject enemyObject);
}
