using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState{
    idle,
    walk,
    attack,
    knockBack,
    carry
}

public abstract class MovingObject : MonoBehaviour
{
    public float   speed;

    [HideInInspector]
    public Vector3     changePos;

    [HideInInspector]
    public ObjectState currentState;

    [HideInInspector]
    public Animator    anime;

    private Rigidbody2D   rb2d;

    protected virtual void Start()
    {
        currentState = ObjectState.idle;
        anime =        GetComponent<Animator>();
        rb2d =         GetComponent<Rigidbody2D>();
    }

    public virtual void MainController()
    {
        if (changePos != Vector3.zero && currentState != ObjectState.attack)
            MoveObject();
        else
            AnimationIdle();
    }

    public Vector3 GetPosition(){
        return (transform.position);
    }

    protected void MoveObject()
    {
        changePos.Normalize();
        rb2d.MovePosition(transform.position + changePos * speed * Time.deltaTime);
        AnimationMovement();
    }

    protected void AnimationIdle(){
        anime.SetBool("Moving", false);
        if (currentState != ObjectState.carry)
            currentState = ObjectState.idle;
    }

    protected void AnimationMovement()
    {
        anime.SetFloat("DirectionX", changePos.x);
        anime.SetFloat("DirectionY", changePos.y);
        anime.SetBool("Moving", true);
        if (currentState == ObjectState.carry)
            return;

        currentState = ObjectState.walk;
    }
}