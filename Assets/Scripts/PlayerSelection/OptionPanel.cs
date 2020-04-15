using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] internal List<GameObject> m_PlayerSelectButtonGameObjectList;
    [SerializeField] internal GameObject m_SelectedPlayerSelectButtonGameObject;

    // Start is called before the first frame update
    void Start()
    {
        if (m_SelectedPlayerSelectButtonGameObject != null)
        {
            GameObject defaultSelectedButton = m_PlayerSelectButtonGameObjectList[0];
            SelectButton(defaultSelectedButton);
        }
    }

    public GameObject SelectedPlayerSelectButtonGameObject 
    {
        get => m_SelectedPlayerSelectButtonGameObject;
        set => m_SelectedPlayerSelectButtonGameObject = value;
    }

    public void SelectButton(GameObject playerSelectButtonGameObject) 
    {
        PlayerSelectionButton playerSelectionButton = playerSelectButtonGameObject.GetComponent<PlayerSelectionButton>();
        if (m_SelectedPlayerSelectButtonGameObject != null)
            DeselectButton(m_SelectedPlayerSelectButtonGameObject);
        playerSelectionButton.Select(true);
        m_SelectedPlayerSelectButtonGameObject = playerSelectButtonGameObject;
    }

    public void DeselectButton(GameObject playerSelectButtonGameObject) 
    {
        PlayerSelectionButton playerSelectionButton = playerSelectButtonGameObject.GetComponent<PlayerSelectionButton>();
        playerSelectionButton.Select(false);
        m_SelectedPlayerSelectButtonGameObject = null;
    }

    public PlayerSelectionButton GetSelectedButton() 
    {
        PlayerSelectionButton playerSelectionButton = m_SelectedPlayerSelectButtonGameObject.GetComponent<PlayerSelectionButton>();
        return playerSelectionButton;
    }
}
