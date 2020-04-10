using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyUI : MonoBehaviour
{
    public static RubySystem rubySystemStatic;
    public RubySystem rubySystem;

    private int _ruby;
    private AudioSource _audio = null;

    [Header("Attached result player")]
    [SerializeField]
    public GameObject _resultPlayer;

    // Position data
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;
    

    private void Start()
    {
        rubySystem = new RubySystem(_resultPlayer.GetComponent<ResultPlayer>().rubyInit);
        _audio = GetComponent<AudioSource>();
        InitRubyUI();

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
            return 0.38f * side;
        return 0.33f;
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
            RubyUI rubyTextUi = transform.parent.transform.parent.transform.parent.transform.parent.Find("Player_1").Find("UI").Find("Rubies").Find("RubyTextUI").GetComponent<RubyUI>();
            float xValue = rubyTextUi.ResultRect.localPosition.x;
            if (playerIndex == 2 || playerIndex == 4)
                xValue *= -1.025f;

            _resultRect.localPosition = new Vector3(xValue,
                                                    rubyTextUi.ResultRect.localPosition.y,
                                                    rubyTextUi.ResultRect.localPosition.z);
        }
    }

    public void InitRubyUI()
    {
        _ruby = _resultPlayer.GetComponent<ResultPlayer>().rubyInit;
        GetComponent<Text>().text = _ruby.ToString("00");
        rubySystemStatic = rubySystem;

        rubySystem.OnDecrease += RefreshRuby;
        rubySystem.OnIncrease += RefreshRuby;
    }

    private void RefreshRuby(object sender, System.EventArgs e){
        StartCoroutine(RubyCo());
    }

    private IEnumerator RubyCo()
    {
        int systemValue = rubySystem.GetValue();
        if (_ruby < systemValue)
        {
            while(_ruby < systemValue)
            {
                _audio.Play();
                _ruby++;
                GetComponent<Text>().text = _ruby.ToString("00");
                yield return new WaitForSeconds(0.07f);
            }
        }
        else
        {
            while(_ruby > systemValue)
            {
                _audio.Play();
                _ruby--;
                GetComponent<Text>().text = _ruby.ToString("00");
                yield return new WaitForSeconds(0.07f);

            }
        }
    }

    public RectTransform ResultRect {
        get { return _resultRect; }
    }
}
