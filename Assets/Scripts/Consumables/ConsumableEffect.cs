using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableEffect : MonoBehaviour
{
    public ConsumableEffect(){}

    public void LaunchEffect(string effect) {
        switch(effect) {
            case "banana":
                _EffectBanana();
                break;
            case "green_shell":
                _EffectGreenShell();
                break;
            default:
                break;
        }
    }

    void _EffectBanana() {
        Debug.Log("Banana !");
    }

    void _EffectGreenShell() {
        Debug.Log("Green Shell !");
    }
}
