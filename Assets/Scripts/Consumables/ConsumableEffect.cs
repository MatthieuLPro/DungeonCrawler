using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableEffect : MonoBehaviour
{
    private Vector3 _playerPosition = Vector3.zero;
    private string _playerDirection = "";
    public ConsumableEffect(){}

    public void LaunchEffect(string effect, Vector3 playerPosition, string playerDirection) {
        PlayerPosition = playerPosition;
        PlayerDirection = playerDirection;

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
        _InstantiateObject("banana");
    }

    void _EffectGreenShell() {
        Debug.Log("Green Shell !");
    }

    /* ************************************************ */
    /* Create functions */
    /* ************************************************ */
    // Generate object depending of settings
    private GameObject _InstantiateObject(string consumableName)
    {
        GameObject newPrefab   = _GetResource(consumableName);
        Vector3 positionOffset = Vector3.zero;

        if (PlayerDirection == "up")
            positionOffset = new Vector3(0, 0.5f, 0);
        else if (PlayerDirection == "right")
            positionOffset = new Vector3(-0.5f, 0, 0);
        else if (PlayerDirection == "down")
            positionOffset = new Vector3(0, -0.5f, 0);
        else 
            positionOffset = new Vector3(0.5f, 0, 0);

        return (Instantiate(newPrefab, PlayerPosition, Quaternion.identity));
    }

    // Generate prefab model from resource
    GameObject _GetResource(string consumableName)
    {
        GameObject resource = null;
        switch(consumableName){
            case "banana":
                resource = Resources.Load("Prefabs/Consumables/ConsumableBanana") as GameObject;
                break;
            case "green_shell":
                resource = Resources.Load("Prefabs/Consumables/ConsumableGreenShell") as GameObject;
                break;
        }
        return resource;
    }

    public Vector2 PlayerPosition {
        get { return _playerPosition; }
        set { _playerPosition = value; }
    }

    public string PlayerDirection {
        get { return _playerDirection; }
        set { _playerDirection = value; }
    }
}
