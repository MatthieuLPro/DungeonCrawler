using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField]
    private float thrust = 0.2f;

    [SerializeField]
    private int _damagePower = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("ItemDestructible")){
            other.GetComponent<DestructibleItem>().Smash();
        }
        else if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            Rigidbody2D rb2d = enemy.GetComponent<Rigidbody2D>();
            if(rb2d != null)
            {
                rb2d.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                Debug.Log("hey");
                //enemy.MovePosition(other.gameObject.transform.position + difference * Time.deltaTime);
                rb2d.AddForce(difference * 1000, ForceMode2D.Impulse);
                rb2d.isKinematic = true;
            }
        }  
    }
}