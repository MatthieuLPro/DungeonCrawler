using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    /* ////////////////// */
    // Alphabetical Order
    /* ////////////////// */
    public bool  attackTypeMagic;
    public bool  attackTypePhysic;
    public float knockBackTime;
    public float strength;
    public int   life;

    /* Add damage depending of player str */
    public void looseLife()
    {
        life -= 1;
        if (life <= 0)
            IsDead();
    }

    public void IsDead(){
        Destroy(gameObject);
    }

}
