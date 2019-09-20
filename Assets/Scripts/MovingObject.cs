using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState{
    walk,
    attack
}

public abstract class MovingObject : MonoBehaviour
{
    public float   speed;

    [HideInInspector]
    public Vector3     changePos;
    public ObjectState currentState;
    public Animator    anime;

    private Rigidbody2D   rb2d;

    protected virtual void Start()
    {
        currentState = ObjectState.walk;
        anime =        GetComponent<Animator>();
        rb2d =         GetComponent<Rigidbody2D>();
    }

    protected void SmoothTransition()
    {
        changePos.Normalize();
        rb2d.MovePosition(transform.position + changePos * speed * Time.deltaTime);
    }

    protected void AnimationIdle()
    {
        anime.SetBool("Moving", false);
    }

    protected void AnimationMovement()
    {
        anime.SetFloat("DirectionX", changePos.x);
        anime.SetFloat("DirectionY", changePos.y);
        anime.SetBool("Moving", true);
    }

    public void DamageKnockBack(Vector3 knockBackDir, float knockBackDistance, int damage)
    {
        // transform.position += knockBackDir * knockBackDistance;
        Vector2 velocity = new Vector2(500000f, 500000f);
        Debug.Log(rb2d.gameObject.transform.position);
        rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
        Debug.Log(rb2d.position + velocity * Time.fixedDeltaTime);
        Debug.Log(rb2d.gameObject.transform.position);
        // HeartsHealthUI.heartsHealthSystemStatic.Damage(damage);
    }
}
