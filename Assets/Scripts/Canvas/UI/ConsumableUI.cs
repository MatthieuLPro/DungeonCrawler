using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUI : MonoBehaviour
{
    private Vector3 _cameraSize;
    private float _thickness;
    private RectTransform _resultRect;
    private Image _consumableObject = null;
    private Animator _consumableObjectAnime = null;
    private AudioManager _consumableObjectSound = null;

    void Start()
    {
        Rect cameraPixelRect    = transform.parent.transform.parent.Find("Camera").gameObject.GetComponent<Camera>().pixelRect;
        string playerName       = transform.parent.transform.parent.name;

        _cameraSize             = new Vector3(cameraPixelRect.width, cameraPixelRect.height, 0);
        _resultRect             = GetComponent<RectTransform>();
        _thickness              = _resultRect.rect.height;
        _consumableObject       = transform.GetChild(0).GetComponent<Image>();
        _consumableObjectAnime  = transform.GetChild(0).GetComponent<Animator>();
        _consumableObjectSound  = transform.GetChild(0).GetComponent<AudioManager>();

        float xDistance = GetAdaptedDistance(true);
        float yDistance = GetAdaptedDistance(false);

        SetRectLocalPosition(xDistance, yDistance);
    }

    public bool ConsumableExist() {
        return (_consumableObject.sprite != null);
    }

    public void AddConsumable(int consumable) {
        StartCoroutine(_SearchObjectCo(consumable));
    }

    public void RemoveConsumable() {
        _consumableObjectAnime.SetInteger("itemNb", 0);
        _consumableObject.sprite    = null;
        _consumableObject.color     = new Color(255, 255, 255, 0);
    }

    // Get HUD Position
    float GetAdaptedDistance(bool isAxisX, float side = 0f) {
        if (isAxisX) {
            return 0f;
        }
        return 1 / 1.2f;
    }

    void SetRectLocalPosition(float xDistance, float yDistance) {
        _resultRect.localPosition = new Vector3((_cameraSize.x - _thickness) * xDistance,
                                                (_cameraSize.y - _thickness) * yDistance,
                                                _resultRect.localPosition.z);
    }

    IEnumerator _SearchObjectCo(int consumable) {
        _consumableObjectAnime.SetBool("isSearching", true);
        _consumableObjectAnime.SetInteger("itemNb", consumable);
        PlayAudio("search");

        yield return new WaitForSeconds(0.15f);
        _consumableObject.color = new Color(255, 255, 255, 100);

        yield return new WaitForSeconds(3f);

        _consumableObjectAnime.SetBool("isSearching", false);

        yield return new WaitForSeconds(0.5f);
        PlayAudio("find");
    }

    void PlayAudio(string audioClip) {
        _consumableObjectSound.CallAudio(audioClip);
        _consumableObjectSound.PlayAudio();
    }
}
