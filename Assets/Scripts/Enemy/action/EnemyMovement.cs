using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    knockBack,
    carry
}

public abstract class EnemyMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float   speed;

    [HideInInspector]
    public Vector3     changePos;

    [HideInInspector]
    public EnemyState currentState;

    [HideInInspector]
    public Animator    anime;

    [HideInInspector]
    public Rigidbody2D   rb2d;

    private GameObject _parent;
    
    protected virtual void Start()
    {
        currentState    = EnemyState.idle;
        _parent         = transform.parent.gameObject;
        anime           = _parent.GetComponent<Animator>();
        rb2d            = _parent.GetComponent<Rigidbody2D>();
    }

    public virtual void MainController()
    {
        if (changePos != Vector3.zero && currentState != EnemyState.attack)
            MoveObject();
        else
            AnimationIdle();
    }
    
    protected void MoveObject()
    {
        changePos.Normalize();
        rb2d.MovePosition(transform.position + changePos * speed * Time.deltaTime);
        AnimationMovement();
    }

    protected void AnimationIdle(){
        anime.SetBool("Moving", false);
        if (currentState != EnemyState.carry)
            currentState = EnemyState.idle;
    }

    protected void AnimationMovement()
    {
        anime.SetFloat("DirectionX", changePos.x);
        anime.SetFloat("DirectionY", changePos.y);
        anime.SetBool("Moving", true);
        if (currentState == EnemyState.carry)
            return;

        currentState = EnemyState.walk;
    }
}
