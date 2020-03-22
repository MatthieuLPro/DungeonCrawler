using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallKeyTextUI : MonoBehaviour
{
    public static SmallKeySystem smallKeySystemStatic;
    
    [HideInInspector]
    public SmallKeySystem smallKeySystem;

    private int _smallKey;
    private AudioSource _audio = null;

    [Header("Attached player")]
    [SerializeField]
    public GameObject _player;

    // Position data
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;
    

    private void Start()
    {
        smallKeySystem = new SmallKeySystem(_player.GetComponent<Player>().keys);
        _audio = GetComponent<AudioSource>();
        InitSmallKeyUI();

        // Set position
        Camera camera       = transform.parent.transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>();
        string playerName   = transform.parent.transform.parent.transform.parent.name;

        _cameraSize = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);
        _resultRect = GetComponent<RectTransform>();
        _thickness  = _resultRect.rect.height;

        float side      = GetHorizontalSide(playerName);
        float xDistance = GetAdaptedDistance(true, side);
        float yDistance = GetAdaptedDistance(false, side);

        SetRectLocalPosition(xDistance, yDistance);
    }

    // Get HUD Position
    float GetHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    float GetAdaptedDistance(bool isAxisX, float side = 0f) {
        if (isAxisX) {
            //return (1f / 3.05f) * side;
            if (side == 1f)
                return (1f / 1.78f) * side;
            else
                return (1f / 3.05f) * side;
        }
        return 1 / 1.8f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) * xDistance,
                                                (_cameraSize.y - _thickness) * yDistance,
                                                _resultRect.localPosition.z);
    }

    public void InitSmallKeyUI()
    {
        _smallKey = _player.GetComponent<Player>().keys;
        GetComponent<Text>().text = _smallKey.ToString("0");
        smallKeySystemStatic = smallKeySystem;

        smallKeySystem.OnDecrease += RefreshSmallKey;
        smallKeySystem.OnIncrease += RefreshSmallKey;
    }

    private void RefreshSmallKey(object sender, System.EventArgs e){
        StartCoroutine(SmallKeyCo());
    }

    private IEnumerator SmallKeyCo()
    {
        int systemValue = smallKeySystem.GetValue();
        if (_smallKey < systemValue)
        {
            _smallKey++;
            GetComponent<Text>().text = _smallKey.ToString("0");
            yield return new WaitForSeconds(0.07f);
        }
        else
        {
            _smallKey--;
            GetComponent<Text>().text = _smallKey.ToString("0");
            yield return new WaitForSeconds(0.07f);
        }
    }
}
