using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallKeyUI : MonoBehaviour
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

        SetRectLocalPosition(_resultRect.localPosition.z, GetTextHorizontalSide(playerName));
    }

    // Get HUD Position
    float GetTextHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    void SetRectLocalPosition(float zPosition, float side) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) / 3.2f * side,
                                                (_cameraSize.y - _thickness) / 2.3f,
                                                zPosition);
    }

    public void InitSmallKeyUI()
    {
        _smallKey = _player.GetComponent<Player>().keys;
        GetComponent<Text>().text = _smallKey.ToString("00");
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
            GetComponent<Text>().text = _smallKey.ToString("00");
            yield return new WaitForSeconds(0.07f);
        }
        else
        {
            _smallKey--;
            GetComponent<Text>().text = _smallKey.ToString("00");
            yield return new WaitForSeconds(0.07f);
        }
    }
}
