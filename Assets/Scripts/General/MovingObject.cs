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
    public float   acceleration = 0.0f;
    public float   decceleration = 0.0f;
    public float   maxSpeed = 0.0f;
    public bool    hasManyForce = false;

    [HideInInspector]
    public float maxSpeedTemp;

    [Header("General Knockback parameters")]
    [SerializeField]
    private Color _flashColor = Color.red;
    [SerializeField]
    private Color _regularColor = Color.white;
    [SerializeField]
    private float _flashDuration = .02f;
    [SerializeField]
    private BoxCollider2D _hurtBox = null;
    [SerializeField]
    private SpriteRenderer _sprite = null;

    [HideInInspector]
    public Vector3 changePos = Vector3.zero;

    [HideInInspector]
    public ObjectState currentState;

    [HideInInspector]
    public Animator anime;

    [HideInInspector]
    public Rigidbody2D _rb2d;

    protected virtual void Start()
    {
        currentState = ObjectState.idle;
        anime = GetComponent<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        maxSpeedTemp = maxSpeed;
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
        if(_rb2d.velocity.magnitude > maxSpeedTemp)
            _rb2d.velocity = _rb2d.velocity.normalized * maxSpeedTemp;
        else
            _rb2d.AddForce(changePos * acceleration, ForceMode2D.Impulse);
        AnimationMovement();
    }

    protected void Decceleration()
    {
        if (_rb2d.velocity == Vector2.zero)
            return;

        if ( _rb2d.velocity.x >= -decceleration && _rb2d.velocity.x <= decceleration && _rb2d.velocity.y >= -decceleration && _rb2d.velocity.y <= decceleration)
            _rb2d.velocity = Vector2.zero;

        _rb2d.velocity = _rb2d.velocity.normalized * decceleration;
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