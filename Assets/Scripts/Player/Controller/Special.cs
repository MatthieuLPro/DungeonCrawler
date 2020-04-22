using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Special : MonoBehaviour
{
    private TestInteractionGlobal _interactionGlobal;
    private Player _player;

    void Start() {
        _interactionGlobal = transform.parent.Find("Interaction").Find("Global").GetComponent<TestInteractionGlobal>();
        _player             = transform.parent.transform.parent.GetComponent<Player>();
    }

     public void SpecialsList()
    {
        Debug.Log("Hey dude");
        // Need to verify depending of character
        //GlobalDefense();
    }

    public void GlobalDefense() {
        _interactionGlobal.IsInvincible = !_interactionGlobal.IsInvincible;
        if (_interactionGlobal.IsInvincible)
            _player._isLoosingStamina = true;
        else
            _player._isLoosingStamina = false;        
    }
}
