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
    private GameObject player = null;

    // Position data
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;


    private void Start(){
        _manaBar    = new GameObject("manaBar", typeof(Image));
        manaSystem = new ManaSystem(player.GetComponent<Player>().manaInit);
        ManaDisplay();

        // Set position
        Camera camera = player.transform.Find("Camera").gameObject.GetComponent<Camera>();

        _cameraSize = new Vector3(camera.pixelRect.width, camera.pixelRect.height, 0);
        _resultRect = GetComponent<RectTransform>();
        _thickness  = _resultRect.rect.height;

        SetRectLocalPosition(_resultRect.localPosition.z, GetTextHorizontalSide(player.name));
    }
    
    // Get HUD Position
    float GetTextHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    void SetRectLocalPosition(float zPosition, float side) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) / 1.75f * side,
                                                (_cameraSize.y - _thickness) / 2f,
                                                zPosition);
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
        _manaBar.GetComponent<RectTransform>().sizeDelta = new Vector2(45, manaValue);
    }
}
