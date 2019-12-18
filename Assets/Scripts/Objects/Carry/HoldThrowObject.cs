using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldThrowObject : MonoBehaviour
{
    private GameObject  _carrier = null;
    private Animator    _anime;
    private float       _speed = 2.0f;
    private Vector3     _direction;

    public void SetCarrier(GameObject carrier)
    {
        _carrier = carrier;
        _anime = _carrier.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_anime.GetBool("Carrying")== false)
        {
            Throw();
            return;
        }

        if (InputManagerPlayer1.YButton() || InputManagerPlayer1.BButton())
        {
            _direction = new Vector3(_anime.GetFloat("DirectionX"), _anime.GetFloat("DirectionY"), 0);
            _direction.Normalize();
            StartCoroutine(ThrowItem());
        }
        else if (transform.position != _carrier.transform.position + new Vector3(0, 0.1f, 0))
            transform.position = _carrier.transform.position + new Vector3(0, 0.1f, 0);
    }

    private IEnumerator ThrowItem()
    {
        _anime.SetBool("Carrying", false);
        yield return new WaitForSeconds(0.4f);

        Destroy(gameObject);
    }

    private void Throw()
    {
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + _direction * _speed * Time.deltaTime);
        if (_speed > 0)
            _speed -= 0.01f;
    }
}