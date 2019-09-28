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
    [Header("General Movement parameters")]
    public float   speed;

    [Header("General Knockback parameters")]
    public Color _flashColor;
    public Color _regularColor;
    public float _flashDuration;
    public int _nbFlash;
    public BoxCollider2D _hurtBox;
    public SpriteRenderer _sprite;

    [HideInInspector]
    public Vector3     changePos;

    [HideInInspector]
    public ObjectState currentState;

    [HideInInspector]
    public Animator    anime;

    private Rigidbody2D   _rb2d;

    protected virtual void Start()
    {
        currentState = ObjectState.idle;
        anime =        GetComponent<Animator>();
        _rb2d =         GetComponent<Rigidbody2D>();
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
        _rb2d.MovePosition(transform.position + changePos * speed * Time.deltaTime);
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

    public IEnumerator FlashCo()
    {
        int temp = 0;
        _hurtBox.enabled = false;
        while(temp < _nbFlash)
        {
            _sprite.color = _flashColor;
            yield return new WaitForSeconds(_flashDuration);
            _sprite.color = _regularColor;
            yield return new WaitForSeconds(_flashDuration);
            temp++;
        }
        _hurtBox.enabled = true;
    }
}