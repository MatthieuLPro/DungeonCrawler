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
    public int strength;
    public float thrust;
    public int   life;

    public void ManageLife(int damage){
        UpdateLife(damage);
        VerifyLife();
    }

    /* Add damage depending of player str */
    private void UpdateLife(int damage){
        life -= damage;
    }

    private void VerifyLife()
    {
        if (life <= 0)
            StartCoroutine(DestroyEnemyCo());
    }

    private IEnumerator DestroyEnemyCo()
    {
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(2).GetComponent<AudioManager>().CallAudio("Ko");
        
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    /* ************************************************ */
    /* Getter */
    /* ************************************************ */
    public int GetStrength(){
        return strength;
    }

    public float GetThrust(){
        return thrust;
    }

    public float GetKnockBackTime(){
        return knockBackTime;
    }

    public bool GetAttackTypePhysic(){
        return attackTypePhysic;
    }    

    public bool GetAttackTypeMagic(){
        return attackTypeMagic;
    }    

}
