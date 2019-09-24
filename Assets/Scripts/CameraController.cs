using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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

    [SerializeField]
    private float _cameraHeight;
    [SerializeField]
    private float _cameraWidth;

    private enum EcameraScrollDirection 
    {
        top,
        bottom,
        left,
        right
    };

    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        _enabledCameraScroll = true;
        _boxCollider2D       = gameObject.GetComponent<BoxCollider2D>();
        _Camera              = gameObject.GetComponent<Camera>();

        _InitializeCamera();
        
    }

    void FixedUpdate(){
        _UpdateCameraPosition(false);
    }

    private void _InitializeCamera()
    {
        // Get Camera size informations
        _cameraHeight = _Camera.orthographicSize * 2f;
        _cameraWidth = (_Camera.aspect * _cameraHeight);

        // Initialize Box Collider
        _boxCollider2D.size = new Vector2(_Camera.aspect * 2f * _Camera.orthographicSize, 2f * _Camera.orthographicSize);
        _UpdateCameraPosition(true);
    }

    private void _UpdateCameraPosition(bool _forceUpdate)
    {
        PlayerController playerController = trackingGameObject.GetComponent<PlayerController>();
        if (_forceUpdate || (_enabledCameraScroll && playerController.isWalking))
        {
            
            float xPosition = Mathf.Clamp(trackingGameObject.transform.position.x, _xMinLimit, _xMaxLimit);
            float yPosition = Mathf.Clamp(trackingGameObject.transform.position.y, _yMinLimit, _yMaxLimit);
            /*Vector3 _CameraPosition = new Vector3(xPosition, yPosition, transform.position.z);

            transform.position = _CameraPosition;*/
            Vector3 targetPosition = trackingGameObject.transform.TransformPoint(new Vector3(xPosition, yPosition, transform.position.z));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        }
    }

    private void _CameraScrollTo(EcameraScrollDirection _direction)
    {
        float xPosition = transform.position.x;
        float yPosition = transform.position.y;
        switch (_direction)
        {
            case EcameraScrollDirection.top:
                yPosition += _cameraHeight;
                break;
            case EcameraScrollDirection.bottom:
                yPosition -= _cameraHeight;
                break;
            case EcameraScrollDirection.left:
                xPosition -= _cameraWidth;
                break;
            case EcameraScrollDirection.right:
                xPosition += _cameraWidth;
                break;
        }
        transform.position = new Vector3(xPosition, yPosition, transform.position.z);
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
