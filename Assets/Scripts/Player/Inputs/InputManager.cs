﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerProfile playerProfile = null;
    public GameObject movementGO;
    public GameObject attackGO;
    public GameObject consumableGO;
    public GameObject ActionGO;

    void Awake() {
        playerProfile = transform.parent.GetComponent<PlayerProfile>();
    }

    private void OnMove(InputValue value)
    {
        Movement movement = movementGO.GetComponent<Movement>();
        Vector2 newMovement = value.Get<Vector2>();
        if (playerProfile != null)
            playerProfile.NbStep = 1;
        movement.SetPlayerDirection(newMovement);
    }

    public void OnAButton()
    {
        Attack attack = attackGO.GetComponent<Attack>();
        if (playerProfile != null)
            playerProfile.NbAttack = 1;
        attack.ActionsList();
    }

    public void OnBButton()
    {
        Action action = ActionGO.GetComponent<Action>();
        action.ActionsList();
    }

    public void OnYButton()
    {
        UseConsumable useConsumable = consumableGO.GetComponent<UseConsumable>();
        useConsumable.launchCurrentEffect();
    }

    /*

    public void OnYButton()
    {
        Debug.Log("X");
    }

    public void OnYButton()
    {
        Debug.Log("Y");
    }

    public void OnLeftButton()
    {
        Debug.Log("LeftButton");
    }

    public void OnLeftTrigger()
    {
        Debug.Log("OnLeftTrigger");
    }

    public void OnRightButton()
    {
        Debug.Log("RightButton");
    }

    private void OnRightTrigger()
    {
        Debug.Log("RightTrigger");
    }*/
}
