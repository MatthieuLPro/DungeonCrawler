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

    public void ManageLife(){
        UpdateLife();
        VerifyLife();
    }

    /* Add damage depending of player str */
    private void UpdateLife(){
        life -= 1;
    }

    private void VerifyLife()
    {
        if (life <= 0)
            StartCoroutine(DestroyEnemyCo());
    }

    private IEnumerator DestroyEnemyCo()
    {
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<AudioManager>().CallAudio("Ko");
        
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

}
