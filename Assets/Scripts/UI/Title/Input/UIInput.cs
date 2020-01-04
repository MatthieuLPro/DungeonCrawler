using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UIInput : MonoBehaviour
{
    public AudioClip OnValidateSound;
    public AudioClip OnFocusSound;
    public bool IsFocus;
    public MenuState MenuState;
    public AudioSource Source { get { return GetComponent<AudioSource>(); } }

    
    private Color32 _SelectedColor = new Color32(254, 174, 52, 255);
    private Color32 _UnselectedColor = new Color32(192, 203, 220, 255);

    void Start() 
    {
        MenuState = transform.parent.gameObject.GetComponent<MenuState>();

        // Initialize Sound effect
        gameObject.AddComponent<AudioSource>();
        Source.playOnAwake = false;
    }

    public virtual void InputValidation() {}

    public void SetFocus(bool IsFocus)
    {
        if (IsFocus) 
            Source.PlayOneShot(OnFocusSound);

        Color32 newColor = IsFocus ? _SelectedColor : _UnselectedColor;
        Text Label = transform.GetChild(0).GetComponent<Text>();
        Label.color = newColor;
    }

    public void Activate() 
    {
        Source.PlayOneShot(OnValidateSound);
        InputValidation();
    }
}
