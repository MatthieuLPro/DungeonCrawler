using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableUI : MonoBehaviour
{
    private Image _consumableObject = null;
    private Animator _consumableObjectAnime = null;
    private AudioManager _consumableObjectSound = null;

    void Start()
    {
        _consumableObject       = transform.GetChild(0).GetComponent<Image>();
        _consumableObjectAnime  = transform.GetChild(0).GetComponent<Animator>();
        _consumableObjectSound  = transform.GetChild(0).GetComponent<AudioManager>();
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
