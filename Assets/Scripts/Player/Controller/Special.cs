using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Special : MonoBehaviour
{
    private TestInteractionGlobal _interactionGlobal = null;
    private Player _player = null;
    private Movement _movement = null;
    private bool _isPressed = false;

    void Start() {
        _interactionGlobal  = transform.parent.Find("Interaction").Find("Global").GetComponent<TestInteractionGlobal>();
        _player             = transform.parent.transform.parent.GetComponent<Player>();
        _movement           = transform.parent.Find("Movement").GetComponent<Movement>();
    }

    void Update() {
        if (_player.Stamina <= 0)
            _CancelSpecial();
    }

     public void SpecialsList(){
        if (_player.CharacterType == "LinkGreen")
            Sprint();
        else if (_player.CharacterType == "LinkBlue")
            GlobalDefense();       
    }

    void _CancelSpecial() {
        if (_player.CharacterType == "LinkGreen")
            Sprint();
        else if (_player.CharacterType == "LinkBlue")
            GlobalDefense();     
    }    

    public void Sprint() {
        if (_player.Stamina <= 0) {
            _SprintFinish();
            return;
        }
        _toggleIsPressed();
        if (IsPressed) {
            _SprintBegin();
        } else {
            _SprintFinish();
        }
    }

    void _SprintBegin() {
        _player.IsLoosingStamina    = true;
        _movement.MaxSpeedTemp      = _movement.MaxSpeed * 2f;
    }

    void _SprintFinish() {
        _player.IsLoosingStamina    = false;
        _movement.MaxSpeedTemp      = _movement.MaxSpeed;
    }

    public void GlobalDefense() {
        _interactionGlobal.IsInvincible = !_interactionGlobal.IsInvincible;
        if (_interactionGlobal.IsInvincible)
            _player.IsLoosingStamina = true;
        else
            _player.IsLoosingStamina = false;        
    }

    private void _toggleIsPressed() {
        IsPressed = !IsPressed;
    }

    public bool IsPressed {
        get => _isPressed;
        set => _isPressed = value;
    }
}
