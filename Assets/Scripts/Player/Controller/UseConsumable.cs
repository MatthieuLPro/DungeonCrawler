using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseConsumable : MonoBehaviour
{
    private string _consumable = "";
    private ConsumableUI _consumablePlayerUI = null;
    private Transform _playerTransform = null;
    private Animator _playerAnimator = null;

    private ConsumableEffect _effect = null;

    void Start() {
        _effect = new ConsumableEffect(transform.parent.transform.parent.Find("ConsumableList").transform);
        _consumablePlayerUI    = transform.parent.transform.parent.Find("UI").Find("Consumable").GetComponent<ConsumableUI>();
        _playerTransform        = transform.parent.transform;
        _playerAnimator        = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManagerPlayer1.XButton() && Consumable != "") {
            _LaunchEffect(Consumable);
        }
    }

    public void SetConsumable(int consumableNb) {
        switch(consumableNb) {
            case 1:
                Consumable = "banana";
                break;
            case 12:
                Consumable = "green_shell";
                break;
            default:
                Consumable = "";
                break;
        }
    }

    void _LaunchEffect(string effect) {
        _effect.LaunchEffect(effect, _GetPosition(), _GetDirection());
        Consumable = "";
        _consumablePlayerUI.RemoveConsumable();
    }

    private string _GetDirection() {
        string playerDirection  = "down";
        float directionX        = _playerAnimator.GetFloat("DirectionX");
        float directionY        = _playerAnimator.GetFloat("DirectionY"); 
        if (directionX >= 0.7f)
        {
            if (directionY >= 0.7f)
                playerDirection = "up-right";
            else if (directionY <= -0.7f)
                playerDirection = "down-right";
            else
                playerDirection = "right";
        } else if (directionX <= -0.7f) {
            if (directionY >= 0.7f)
                playerDirection = "up-left";
            else if (directionY <= -0.7f)
                playerDirection = "down-left";
            else
                playerDirection = "left";
        } else {
            if (directionY >= 0.7f)
                playerDirection = "up";
            else
                playerDirection = "down";
        }
        return playerDirection;
    }

    private Vector3 _GetPosition() {
        return _playerTransform.position;
    }

    public string Consumable {
        get { return _consumable; }
        set { _consumable = value; }
    }
}
