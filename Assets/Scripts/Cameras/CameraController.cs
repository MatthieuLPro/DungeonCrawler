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

    public Vector2 minPosition;
    public Vector2 maxPosition;

    private BoxCollider2D _boxCollider2D;
    private Camera _Camera;

    private float _xScreenDistance;
    private float _yScreenDistance;

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

        SetScreenDistance();
        _UpdateCameraPosition(true);
    }

    void LateUpdate(){
        _UpdateCameraPosition(false);
    }

    public void enabledCameraScroll(bool _isEnabled){
        _enabledCameraScroll = _isEnabled;
    }

    private void _UpdateCameraPosition(bool _forceUpdate)
    {
        if (_forceUpdate || (_enabledCameraScroll))
        {
            float xPosition = Mathf.Clamp(trackingGameObject.transform.position.x, minPosition.x + _xScreenDistance, maxPosition.x - _xScreenDistance);
            float yPosition = Mathf.Clamp(trackingGameObject.transform.position.y, minPosition.y - _yScreenDistance, maxPosition.y + _yScreenDistance);
            moveCameraToPosition(xPosition, yPosition);
        }  
    }

    public void moveCameraToPosition(float _XPosition, float _YPosition)
    {
        Vector3 targetPosition = new Vector3(_XPosition, _YPosition, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

    public void updateMinMaxLimits()
    {
        minPosition = _RoomInfo.getRoomLimits()[0];
        maxPosition = _RoomInfo.getRoomLimits()[1];
    }

    public void SetScreenDistance()
    {
        Vector3 newMaxPosition = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelRect.width, _Camera.pixelRect.height));
        _xScreenDistance = newMaxPosition.x;
        _yScreenDistance = newMaxPosition.y;
    }
}
