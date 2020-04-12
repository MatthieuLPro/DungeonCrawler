using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    public static ManaSystem manaSystemStatic;
    public ManaSystem manaSystem;

    [SerializeField]
    private Sprite _manaBarSprite = null;

    private GameObject _manaBar;

    [Header("Attached player")]
    [SerializeField]
    private GameObject _player = null;

    // Position data
    private Vector3 _cameraSize;
    private float _width;
    private float _height;
    private RectTransform _resultRect;


    private void Start(){
        if (_player == null)
            _player = transform.parent.transform.parent.transform.parent.gameObject;
        _manaBar    = new GameObject("manaBar", typeof(Image));
        manaSystem = new ManaSystem(_player.GetComponent<Player>().manaInit);
        ManaDisplay();

        // Set position
        Camera camera = _player.transform.Find("Camera").gameObject.GetComponent<Camera>();

        _cameraSize = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);
        _resultRect = GetComponent<RectTransform>();
        _height     = _resultRect.rect.height;
        _width      = _resultRect.rect.width;

        float side      = GetHorizontalSide(_player.name);
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
        if (isAxisX) {
            return 0.9f * side;
        }
        return 0.9f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        int playerIndex     = _player.GetComponent<Player>().PlayerIndex;
        int playersNumber   = transform.root.Find("GameParameters").GetComponent<GameParameters>().PlayersNumber;

        if (playerIndex == 1) {
            float xValue = _cameraSize.x;
            float yValue = _cameraSize.y;
            if (playersNumber > 1) {
                xValue /= 2;
                yValue /= 2;
            }
            _resultRect.localPosition = new Vector3((xValue - _width) * xDistance,
                                                    (yValue - _height) * yDistance,
                                                    _resultRect.localPosition.z);
        } else {
            ManaUI manaUi = transform.parent.transform.parent.transform.parent.Find("Player_1").Find("UI").Find("Manas").GetComponent<ManaUI>();
            float xValue = manaUi.ResultRect.localPosition.x;
            if (playerIndex == 2 || playerIndex == 4)
                xValue *= -0.775f;

            _resultRect.localPosition = new Vector3(xValue,
                                                    manaUi.ResultRect.localPosition.y,
                                                    manaUi.ResultRect.localPosition.z);
        }
    }

    // Mana HUD
    private void ManaDisplay()
    {
        Image manaBarUI = _manaBar.GetComponent<Image>();

        manaBarUI.sprite = _manaBarSprite;
        InitManaUI();
    }

    public void InitManaUI()
    {
        int manaValue = manaSystem.GetMana();
        manaSystemStatic = manaSystem;

        _manaBar.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        SetPositionManaUI(manaValue);
    
        manaSystem.OnDecrease += RefreshMana;
        manaSystem.OnIncrease += RefreshMana;
    }

    private void RefreshMana(object sender, System.EventArgs e)
    {
        int manaValue = manaSystem.GetMana();

        SetPositionManaUI(manaValue);
    }

    private void SetPositionManaUI(int manaValue)
    {
        _manaBar.transform.SetParent(transform);
        _manaBar.transform.localPosition = Vector3.zero;
        _manaBar.transform.localScale = new Vector3(1, 1, 1);
        _manaBar.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        _manaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(34, manaValue);
    }

    public RectTransform ResultRect {
        get { return _resultRect; }
    }
}
