using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;



public class PlayerSelectionButton : MonoBehaviour
{
    [SerializeField] internal TextMesh m_ButtonTextMesh;
    [SerializeField] internal Color m_IdleColor;
    [SerializeField] internal Color m_SelectedColor;
    [SerializeField] internal bool m_IsSelected;
    [SerializeField] internal string m_Value;

    // Optional
    [SerializeField] internal GameObject m_OptionPanelGameObject;

    void Start() 
    {
        Select(false);
    }

    public void OnClickButton() 
    {
        if (m_OptionPanelGameObject == null)
            Select(true);
        else 
        {
            Debug.Log("OnClickButton");
            OptionPanel optionPanel = m_OptionPanelGameObject.GetComponent<OptionPanel>();
            optionPanel.SelectButton(gameObject);
        }
            
    }

    public void Select(bool select) 
    {
        
        if (select) 
        {
            m_ButtonTextMesh.color = m_SelectedColor;
        }
        else
            m_ButtonTextMesh.color = m_IdleColor;
        IsSelected = select;
    }
    public TextMesh ButtonTextMesh
    {
        get => m_ButtonTextMesh;
        set => m_ButtonTextMesh = value;
    }

    public Color IdleColor
    {
        get => m_IdleColor;
        set => m_IdleColor = value;
    }

    public Color SelectedColor
    {
        get => m_SelectedColor;
        set => m_SelectedColor = value;
    }

    public GameObject OptionPanelGameObject
    {
        get => m_OptionPanelGameObject;
        set => m_OptionPanelGameObject = value;
    }

    public bool IsSelected 
    {
        get => m_IsSelected;
        set => m_IsSelected = value;
    }

    public string Value
    {
        get => m_Value;
        set => m_Value = value;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!IsSelected)
            m_ButtonTextMesh.color = m_SelectedColor;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!IsSelected)
            m_ButtonTextMesh.color = m_IdleColor;
    }
}
