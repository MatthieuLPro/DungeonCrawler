using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseConsumable : MonoBehaviour
{
    private string _consumable = "";

    private ConsumableEffect _effect = null;

    void Start() {
        _effect = new ConsumableEffect();
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
        _effect.LaunchEffect(effect);
    }

    public string Consumable {
        get { return _consumable; }
        set { _consumable = value; }
    }
}
