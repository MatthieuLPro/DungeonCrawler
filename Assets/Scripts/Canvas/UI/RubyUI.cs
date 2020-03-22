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

        SetRectLocalPosition(_resultRect.localPosition.z, GetTextHorizontalSide(playerName));
    }

    // Get HUD Position
    float GetTextHorizontalSide(string player) {
        if (player == "Player_1" || player == "Player_3")
            return -1f;
        return 1f;
    }

    void SetRectLocalPosition(float zPosition, float side) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) / 2.15f * side,
                                                (_cameraSize.y - _thickness) / 1.8f,
                                                zPosition);
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
}
