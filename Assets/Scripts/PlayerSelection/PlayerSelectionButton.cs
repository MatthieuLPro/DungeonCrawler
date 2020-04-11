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

    // Optional
    [SerializeField] internal GameObject m_OptionPanelGameObject;

    void Start() 
    {
        SelectButton(false);
    }

    public void OnClickButton() 
    {
        Debug.Log("OnClickButton");
        if (m_OptionPanelGameObject != null)
            SelectButton(true);
        else 
        {
            OptionPanel optionPanel = m_OptionPanelGameObject.GetComponent<OptionPanel>();
            optionPanel.SelectButton(gameObject);
        }
            
    }

    public void SelectButton(bool select) 
    {
        if (select)
            m_ButtonTextMesh.color = m_SelectedColor;
        else
            m_ButtonTextMesh.color = m_IdleColor;
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        m_ButtonTextMesh.color = m_SelectedColor;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        m_ButtonTextMesh.color = m_IdleColor;
    }
}
