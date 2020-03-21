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

    public float _xScreenDistance;
    public float _yScreenDistance;

    private RoomPlayerInformation _roomPlayerInfo;

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
        _roomPlayerInfo         = trackingGameObject.transform.parent.GetComponent<RoomPlayerInformation>();

        SetScreenDistance();
        _InitializeCameraProperties();
        _UpdateCameraPosition(true);
    }

    private void _InitializeCameraProperties()
    {
        int playerIndex = gameObject.transform.parent.gameObject.GetComponent<Player>().GetPlayerIndex();
        int playersNumber = GameObject.Find("GameParameters").GetComponent<GameParameters>().GetPlayersNumber();

        Rect cameraRect = new Rect();

        // Set Camera size
        switch (playersNumber)
        {
            case 1:
            default:
                cameraRect.width = 1;
                cameraRect.height = 1;
                break;
            case 2:
                cameraRect.width = 1;
                cameraRect.height = 0.5f;
                break;
            case 3:
            case 4:
                cameraRect.width = 0.5f;
                cameraRect.height = 0.5f;
                break;
        }

        // Set Position size

        switch (playerIndex)
        {
            case 1:
            default:
                cameraRect.x = 0;
                cameraRect.y = 0;
                break;
            case 2:
                cameraRect.x = (playersNumber == 2) ? 0 : 0.5f; 
                cameraRect.y = (playersNumber == 2) ? 0.5f : 0;
                break;
            case 3:
                cameraRect.x = 0;
                cameraRect.y = 0.5f;
                break;
            case 4:
                cameraRect.x = 0.5f;
                cameraRect.y = 0.5f;
                break;
        }

        _Camera.rect = cameraRect;
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
        Vector2[] newCameraLimits = _roomPlayerInfo.PlayerRoomLimits;
        minPosition = newCameraLimits[0];
        maxPosition = newCameraLimits[1];
    }

    public void SetScreenDistance()
    {
        Vector3 newMaxPosition = _Camera.ScreenToWorldPoint(new Vector2(_Camera.pixelRect.width, _Camera.pixelRect.height));
        _xScreenDistance = newMaxPosition.x;
        _yScreenDistance = newMaxPosition.y;
    }
}
