using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename the class => CameraController ?! (Warning)
public class Camera : MonoBehaviour
{
    public float horizontalScrollSpeed;
    public float verticalScrollSpeed;

    public GameObject trackingGameObject;

    [SerializeField]
    private bool _enabledCameraScroll, _enabledTopScroll,
                 _enabledBottomScroll, _enabledRightScroll,
                 _enabledLeftScroll;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField]
    private float _xMinLimit, _xMaxLimit,
                  _yMinLimit, _yMaxLimit;

    private BoxCollider2D _boxCollider2D;

    private Camera _Camera;

    private float _lastYCameraPosition;
    private float _lastXCameraPosition;

    void Start()
    {
        _enabledCameraScroll = true;
        _boxCollider2D       = gameObject.GetComponent<BoxCollider2D>();
        _Camera              = gameObject.GetComponent<Camera>();
    }

    void FixedUpdate(){
        _UpdateCameraPosition();
    }

    private void _UpdateCameraPosition()
    {
        if (_enabledCameraScroll)
        {
            float xPosition = Mathf.Clamp(trackingGameObject.transform.position.x, _xMinLimit, _xMaxLimit);
            float yPosition = Mathf.Clamp(trackingGameObject.transform.position.y, _yMinLimit, _yMaxLimit);

            Vector3 _CameraPosition = new Vector3(xPosition, yPosition, transform.position.z);
            transform.position = _CameraPosition;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject == trackingGameObject)    
            Physics2D.IgnoreCollision(_boxCollider2D, collision.collider);
        else if (collisionObject.tag == "CameraBorder")
        {
            switch (collisionObject.name)
            {
                case "Top":
                    _yMaxLimit = trackingGameObject.transform.position.y;
                    break;
                case "Bottom":
                    _yMinLimit = trackingGameObject.transform.position.y;
                    break;
                case "Left":
                    _xMinLimit = trackingGameObject.transform.position.x;
                    break;
                case "Right":
                    _xMaxLimit = trackingGameObject.transform.position.x;
                    break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.tag == "CameraBorder")
        {
            switch (collisionObject.name)
            {
                case "Top":
                    _yMaxLimit = 1000f;
                    break;
                case "Bottom":
                    _yMinLimit = -1000f;
                    break;
                case "Left":
                    _xMinLimit = -1000f;
                    break;
                case "Right":
                    _xMaxLimit = 1000f;
                    break;
            }
        }
    }
}
