using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject trackingGameObject;

    [SerializeField]
    private bool _enabledCameraScroll, _enabledTopScroll,
                 _enabledBottomScroll, _enabledRightScroll,
                 _enabledLeftScroll;

    [HideInInspector]
    public Vector2 minPosition;
    public Vector2 maxPosition;

    //[SerializeField]
    //private float _xMinLimit, _xMaxLimit,
    //              _yMinLimit, _yMaxLimit;

    private BoxCollider2D _boxCollider2D;
    private Camera _Camera;

    [SerializeField]
    private float _cameraHeight;
    [SerializeField]
    private float _cameraWidth;

    private RoomInformation _RoomInfo;

    public enum EcameraScrollDirection
    {
        top,
        bottom,
        left,
        right
    };

    public float smoothing = 0.3f;

    void Start()
    {
        _enabledCameraScroll    = true;
        _boxCollider2D          = gameObject.GetComponent<BoxCollider2D>();
        _Camera                 = gameObject.GetComponent<Camera>();
        _RoomInfo               = trackingGameObject.GetComponent<RoomInformation>();

        minPosition = _RoomInfo.getRoomLimits()[0];
        maxPosition = _RoomInfo.getRoomLimits()[1];

        _InitializeCamera();
    }

    void LateUpdate()
    {
        _UpdateCameraPosition(false);
    }

    public void enabledCameraScroll(bool _isEnabled)
    {
        _enabledCameraScroll = _isEnabled;
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
        if (_forceUpdate || (_enabledCameraScroll))
        {
            //float xPosition = Mathf.Clamp(trackingGameObject.transform.position.x, _xMinLimit, _xMaxLimit);
            //float yPosition = Mathf.Clamp(trackingGameObject.transform.position.y, _yMinLimit, _yMaxLimit);
            float xPosition = Mathf.Clamp(trackingGameObject.transform.position.x, minPosition.x, maxPosition.x);
            float yPosition = Mathf.Clamp(trackingGameObject.transform.position.y, minPosition.y, maxPosition.y);
            moveCameraToPosition(xPosition, yPosition);
        }  
    }


    public void moveCameraToPosition(float _XPosition, float _YPosition)
    {
        Vector3 targetPosition = new Vector3(_XPosition, _YPosition, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

    public void _CameraScrollTo(EcameraScrollDirection _direction)
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
        moveCameraToPosition(xPosition, yPosition);
    }

    public void updateMinMaxLimits()
    {
        minPosition = _RoomInfo.getRoomLimits()[0];
        maxPosition = _RoomInfo.getRoomLimits()[1];
    }

/*
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
*/
}
