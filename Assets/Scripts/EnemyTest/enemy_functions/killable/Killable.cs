using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour, IKillable
{
    public bool IsDead(int actualHealth){
        return (actualHealth <= 0);
    }

    public void AnimationDead(GameObject enemyObject){
        Destroy(enemyObject);
    }

    private IEnumerator _AnimationDeadCo(){
        yield return new WaitForSeconds(0.07f);
    }
}
