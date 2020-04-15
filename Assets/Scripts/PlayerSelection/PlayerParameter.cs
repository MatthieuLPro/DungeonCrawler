using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerParameter : MonoBehaviour
{
    [SerializeField] internal int m_Index;
    [SerializeField] internal PlayerInput m_PlayerInputDevice;
    [SerializeField] internal string m_Fighter;

    public int Index
    {
        get => m_Index;
        set => m_Index = value;
    }

    public PlayerInput PlayerInputDevice
    {
        get => m_PlayerInputDevice;
        set => m_PlayerInputDevice = value;
    }

    public string Fighter
    {
        get => m_Fighter;
        set => m_Fighter = value;
    }
}
