using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    public void Smash(){
        animator.SetBool("destroying", true);
        Destroy(GetComponent<BoxCollider2D>());
    }
}
