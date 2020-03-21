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

    public Vector2 topLeftLimit = Vector2.zero;
    public Vector2 topRightLimit = Vector2.zero;
    public Vector2 botLeftLimit = Vector2.zero;
    public Vector2 botRightLimit = Vector2.zero;

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
        float xPosition =  0.0f;
        float yPosition =  0.0f;

        Vector2 maxLimit = Vector2.zero;
        Vector2 minLimit = Vector2.zero;

        if (_forceUpdate || (_enabledCameraScroll))
        {
            if (minPosition.x == 0f)
                minLimit.x = -1000f;
            else
                minLimit.x = minPosition.x + _xScreenDistance;

            if (minPosition.y == 0f)
                minLimit.y = -1000f;
            else
                minLimit.y = minPosition.y - _yScreenDistance;

            if (maxPosition.x == 0f)
                maxLimit.x = 1000f;
            else
                maxLimit.x = maxPosition.x - _xScreenDistance;

            if (maxPosition.y == 0f)
                maxLimit.y = 1000f;
            else
                maxLimit.y = maxPosition.y + _yScreenDistance;
            xPosition = Mathf.Clamp(trackingGameObject.transform.position.x, minLimit.x, maxLimit.x);
            yPosition = Mathf.Clamp(trackingGameObject.transform.position.y, minLimit.y, maxLimit.y);
            moveCameraToPosition(xPosition, yPosition);
        }  
    }

    public void moveCameraToPosition(float _XPosition, float _YPosition)
    {//
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
