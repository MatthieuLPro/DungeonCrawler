﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableEffect : MonoBehaviour
{
    private Vector2 _playerPosition = Vector2.zero;
    private string _playerDirection = "";
    private Transform _consumableList = null;
    public ConsumableEffect(Transform consumableList){
        ConsumableList = consumableList;
    }

    public void LaunchEffect(string effect, Vector2 playerPosition, string playerDirection) {
        PlayerPosition = playerPosition;
        PlayerDirection = playerDirection;

        _EffectConsumable(effect);
    }

    void _EffectConsumable(string effect) {
        GameObject consumablePrefab = _InstantiateObject(effect);
        _SetPrefabParent(consumablePrefab);
        _SetPrefabMvtDirection(effect, consumablePrefab);
    }

    /* ************************************************ */
    /* Create functions */
    /* ************************************************ */
    // Generate object depending of settings
    private GameObject _InstantiateObject(string consumableName){
        return (Instantiate(_GetResource(consumableName), PlayerPosition + _GetPositionOffset(consumableName), Quaternion.identity));
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

    Vector2 _GetPositionOffset(string consumableName) {
        Vector2 positionOffset = Vector2.zero;
        float offset = 1f;

        switch(PlayerDirection) {
            case "up":
                positionOffset = new Vector2(0, -1 * offset);
                break;
            case "up-right":
                positionOffset = new Vector2(-1 * offset, -1 * offset);
                break;
            case "right":
                positionOffset = new Vector2(-1 * offset, 0);
                break;
            case "down-right":
                positionOffset = new Vector2(-1 * offset, offset);
                break;
            case "down":
                positionOffset = new Vector2(0, offset);
                break;
            case "down-left":
                positionOffset = new Vector2(offset, offset);
                break;
            case "left":
                positionOffset = new Vector2(offset, 0);
                break;
            default:
                positionOffset = new Vector2(offset, -1 * offset);
                break;
        }
        return _SetThrowDirection(consumableName, positionOffset);
    }

    void _SetPrefabParent(GameObject instancePrefab) {
        instancePrefab.transform.SetParent(ConsumableList);
    }

    void _SetPrefabMvtDirection(string consumableName, GameObject instancePrefab) {
        Vector2 direction = _GetPositionOffset(consumableName);
        switch (consumableName) {
            case "green_shell":
                instancePrefab.transform.Find("Movement").Find("Direction").GetComponent<DeterminateSingleDirections>().DirectionX = direction.x;
                instancePrefab.transform.Find("Movement").Find("Direction").GetComponent<DeterminateSingleDirections>().DirectionY = direction.y;
                break;
            default:
                break;
        }
    }

    Vector2 _SetThrowDirection(string consumableName, Vector2 positionOffset) {
        switch (consumableName) {
            case "green_shell":
                return (positionOffset * -1.5f);
                break;
            default:
                return positionOffset;
                break;
        }
    }

    public Vector2 PlayerPosition {
        get { return _playerPosition; }
        set { _playerPosition = value; }
    }

    public string PlayerDirection {
        get { return _playerDirection; }
        set { _playerDirection = value; }
    }

    public Transform ConsumableList {
        get { return _consumableList; }
        set { _consumableList = value; }
    }
}
