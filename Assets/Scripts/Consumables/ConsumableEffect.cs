using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableEffect : MonoBehaviour
{
    private Vector2 _playerPosition = Vector2.zero;
    private string _playerDirection = "";
    public ConsumableEffect(){}

    public void LaunchEffect(string effect, Vector2 playerPosition, string playerDirection) {
        PlayerPosition = playerPosition;
        PlayerDirection = playerDirection;

        _EffectConsumable(effect);
    }

    void _EffectConsumable(string effect) {
        GameObject consumablePrefab = _InstantiateObject(effect);
        _SetPrefabParent(consumablePrefab);
    }

    /* ************************************************ */
    /* Create functions */
    /* ************************************************ */
    // Generate object depending of settings
    private GameObject _InstantiateObject(string consumableName){
        return (Instantiate(_GetResource(consumableName), PlayerPosition + _GetPositionOffset(), Quaternion.identity));
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

    Vector2 _GetPositionOffset() {
        Vector2 positionOffset = Vector2.zero;

        switch(PlayerDirection) {
            case "up":
                positionOffset = new Vector2(0, -0.5f);
                break;
            case "up-right":
                positionOffset = new Vector2(-0.5f, -0.5f);
                break;
            case "right":
                positionOffset = new Vector2(-0.5f, 0);
                break;
            case "down-right":
                positionOffset = new Vector2(-0.5f, 0.5f);
                break;
            case "down":
                positionOffset = new Vector2(0, 0.5f);
                break;
            case "down-left":
                positionOffset = new Vector2(0.5f, 0.5f);
                break;
            case "left":
                positionOffset = new Vector2(0.5f, 0);
                break;
            default:
                positionOffset = new Vector2(0.5f, 0.5f);
                break;
        }
        return positionOffset;
    }

    void _SetPrefabParent(GameObject instancePrefab) {
        return; //instancePrefab.transform.SetParent()
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
