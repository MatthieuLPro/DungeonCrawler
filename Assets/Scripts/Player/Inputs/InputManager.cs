using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject movementGO;
    public GameObject attackGO;

    private void OnMove(InputValue value)
    {
        Movement movement = movementGO.GetComponent<Movement>();
        Vector2 newMovement = value.Get<Vector2>();
        movement.SetPlayerDirection(newMovement);
    }

    public void OnBButton()
    {
        Attack attack = attackGO.GetComponent<Attack>();
        attack.ActionsList();

    }
    

    /*public void OnAButton()
    {
        Debug.Log("A");
    }

    

    public void OnXButton()
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
