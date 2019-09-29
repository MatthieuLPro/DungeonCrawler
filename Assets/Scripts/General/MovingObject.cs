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
    [SerializeField]
    private Color _flashColor;
    [SerializeField]
    private Color _regularColor;
    [SerializeField]
    private float _flashDuration = .02f;
    [SerializeField]
    private BoxCollider2D _hurtBox;
    [SerializeField]
    private SpriteRenderer _sprite;

    [HideInInspector]
    public Vector3     changePos;

    [HideInInspector]
    public ObjectState currentState;

    [HideInInspector]
    public Animator    anime;

    [HideInInspector]
    public Rigidbody2D   _rb2d;

    protected virtual void Start()
    {
        currentState = ObjectState.idle;
        anime = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
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

    public IEnumerator FlashCo(float knockTime)
    {
        int temp = 0;
        _hurtBox.enabled = false;
        while(temp < knockTime)
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