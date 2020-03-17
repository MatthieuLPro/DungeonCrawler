using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigKeyUI : MonoBehaviour
{
    public static BigKeySystem bigKeySystemStatic;
    
    [HideInInspector]
    public BigKeySystem bigKeySystem;

    private bool _bigKey;
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
        bigKeySystem = new BigKeySystem(_player.GetComponent<Player>().HasBigKey());
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
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) / 4.9f * side,
                                                (_cameraSize.y - _thickness) / 2.3f,
                                                zPosition);
    }

    public void InitSmallKeyUI()
    {
        _bigKey = _HasBigKey();
        GetComponent<Text>().text = _GetCharValue();
        bigKeySystemStatic = bigKeySystem;

        bigKeySystem.OnDecrease += RefreshBigKey;
        bigKeySystem.OnIncrease += RefreshBigKey;
    }

    private bool _HasBigKey(){
        return _player.GetComponent<Player>().HasBigKey();
    }

    private string _GetCharValue()
    {
        string value = "X";

        if (_bigKey) value = "O";
        return value;
    }

    private Color _GetColor()
    {
        Color value = Color.red;

        if (_bigKey) value = Color.green;
        return value;
    }

    private void RefreshBigKey(object sender, System.EventArgs e){
        StartCoroutine(BigKeyCo());
    }

    private IEnumerator BigKeyCo()
    {
        _bigKey = _HasBigKey();
        GetComponent<Text>().text   = _GetCharValue();
        GetComponent<Text>().color  = _GetColor();
        yield return new WaitForSeconds(0.07f);
    }
}
