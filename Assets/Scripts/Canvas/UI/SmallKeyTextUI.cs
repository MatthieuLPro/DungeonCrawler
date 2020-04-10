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

        //SetRectLocalPosition(xDistance, yDistance);
    }

    // Get HUD Position
    float GetHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    float GetAdaptedDistance(bool isAxisX, float side = 0f) {
        if (isAxisX)
            return 0.28f * side;
        return 0.7f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        int playerIndex     = transform.parent.transform.parent.transform.parent.GetComponent<Player>().PlayerIndex;
        int playersNumber   = transform.root.Find("GameParameters").GetComponent<GameParameters>().PlayersNumber;

        if (playerIndex == 1) {
            float xValue = _cameraSize.x;
            float yValue = _cameraSize.y;
            _resultRect.localPosition = new Vector3((xValue - _thickness) * xDistance,
                                                    (yValue - _thickness) * yDistance,
                                                    _resultRect.localPosition.z);
        } else {
            SmallKeyTextUI smallKeyTextUi = transform.parent.transform.parent.transform.parent.transform.parent.Find("Player_1").Find("UI").Find("SmallKeys").Find("SmallKeyTextUI").GetComponent<SmallKeyTextUI>();
            float xValue = smallKeyTextUi.ResultRect.localPosition.x;
            if (playerIndex == 2 || playerIndex == 4)
                xValue *= -1.025f;

            _resultRect.localPosition = new Vector3(xValue,
                                                    smallKeyTextUi.ResultRect.localPosition.y,
                                                    smallKeyTextUi.ResultRect.localPosition.z);
        }
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

    public RectTransform ResultRect {
        get { return _resultRect; }
    }
}
