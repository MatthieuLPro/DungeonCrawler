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
        
        _consumableObject.color = new Color(255, 255, 255, 100);
        StartCoroutine(_SearchObjectCo(consumable));
    }

    public void RemoveConsumable() {
        _consumableObject.sprite = null;
        _consumableObject.color = new Color(255, 255, 255, 0);
    }


    Sprite _GetConsumableSprite(int consumable) {
        Object[] sprites = Resources.LoadAll("Sprites/Hud/consumables/object_consumable");
        if (consumable < 0 || consumable > 15)
            return null;
        return (Sprite)sprites[consumable];
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
        _consumableObjectSound.CallAudio("search");
        _consumableObjectSound.PlayAudio();
        yield return new WaitForSeconds(3f);

        _consumableObjectAnime.SetBool("isSearching", false);
        _consumableObject.sprite = _GetConsumableSprite(consumable);
        Debug.Log("consumable: " + consumable);
        Debug.Log("_consumableObject.sprite: " + _consumableObject.sprite);
        yield return new WaitForSeconds(0.2f);
        _consumableObjectSound.CallAudio("find");
        _consumableObjectSound.PlayAudio();
    }
}
