using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    public void Smash(){
        animator.SetBool("destroying", true);
        foreach(BoxCollider2D box in gameObject.GetComponents<BoxCollider2D>()){
            Destroy(box);
        }
    }
}
