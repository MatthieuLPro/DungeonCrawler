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
    private Animator    _anime;

    void Start() {
        _interactionGlobal  = transform.parent.Find("Interaction").Find("Global").GetComponent<TestInteractionGlobal>();
        _player             = transform.parent.transform.parent.GetComponent<Player>();
        _movement           = transform.parent.Find("Movement").GetComponent<Movement>();
        _anime              = transform.parent.GetComponent<Animator>();
    }

    void Update() {
        if (_player.Stamina <= 0)
            _CancelSpecial();
    }

     public void SpecialsList(){
        if (_player.CharacterType == "LinkGreen")
            Sprint();
        else if (_player.CharacterType == "LinkBlue")
            Dash();
        else if (_player.CharacterType == "LinkPurple")
            GlobalDefense();       
    }

    void _CancelSpecial() {
        if (_player.CharacterType == "LinkGreen")
            Sprint();
        else if (_player.CharacterType == "LinkBlue")
            Dash();
        else if (_player.CharacterType == "LinkPurple")
            GlobalDefense();     
    }    

    // Sprint functions
    public void Sprint() {
        if (_player.Stamina < 3) {
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
        _anime.SetBool("Specialing", true);
        _anime.SetBool("Moving", false);
        _player.IsLoosingStamina        = true;
        _player.LoosingStaminaDamage    = 3;
        _movement.MaxSpeedTemp          = _movement.MaxSpeed * 1.5f;
    }

    void _SprintFinish() {
        _anime.SetBool("Specialing", false);
        _player.IsLoosingStamina    = false;
        _movement.MaxSpeedTemp      = _movement.MaxSpeed;
    }

    // Dash functions
    public void Dash() {
        if (_player.Stamina < 35) {
            _DashFinish();
            return;
        }
        _toggleIsPressed();
        if (IsPressed) {
            _DashBegin();
        } else {
            _DashFinish();
        }
    }

    void _DashBegin() {
        _anime.SetBool("Specialing", true);
        _anime.SetBool("Moving", false);
        _player.LooseMana(35);
        _movement.IsDashing     = true;
        _movement.MaxSpeedTemp  = _movement.MaxSpeed * 2.5f;
        StartCoroutine(_dashCo());
    }

    void _DashFinish() {
        _anime.SetBool("Specialing", false);
        _movement.IsDashing         = false;
        _movement.MaxSpeedTemp      = _movement.MaxSpeed;
    }

    IEnumerator _dashCo() {
        yield return new WaitForSeconds(0.35f);
        _DashFinish();
    }

    // Global Defense functions
    public void GlobalDefense() {
        if (_player.Stamina < 4) {
            _GlobalDefenseFinish();
            return;
        }
        _toggleIsPressed();
        if (IsPressed) {
            _GlobalDefenseBegin();
        } else {
            _GlobalDefenseFinish();
        }
        StartCoroutine(_GlobalDefenseCo());
    }

    void _GlobalDefenseBegin() {
        _anime.SetBool("Specialing", true);
        _movement.MovementIsBlock       = true;
        _movement.Rb2d.velocity         = Vector3.zero;
        _player.IsLoosingStamina        = true;
        _player.LoosingStaminaDamage    = 4;
        _interactionGlobal.SetPlayerInvincible(true);
    }

    void _GlobalDefenseFinish() {
        _anime.SetBool("Specialing", false);
        _movement.MovementIsBlock       = false;
        _player.IsLoosingStamina        = false;
        _interactionGlobal.SetPlayerInvincible(false);
    }

    IEnumerator _GlobalDefenseCo() {
        yield return new WaitForSeconds(0.5f);
        if (IsPressed == false)
            _GlobalDefenseFinish();
    }

    private void _toggleIsPressed() {
        IsPressed = !IsPressed;
    }

    public bool IsPressed {
        get => _isPressed;
        set => _isPressed = value;
    }
}
