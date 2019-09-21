using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb2d;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void MoveAfterAttack(float thrust){
        if (rb2d == null)
            return;

        rb2d.isKinematic = false;
        //Vector3 difference = rb2d.transform.position - transform.position;
        //difference = difference.normalized * thrust;
        rb2d.AddForce(new Vector3(100, 100, 100), ForceMode2D.Impulse);
        //rb2d.AddForce(transform.up * 5, ForceMode2D.Impulse);
        rb2d.isKinematic = true;
    }
}
