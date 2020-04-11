using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] internal GameObject m_PlayerCursorGameObject;
    [SerializeField] internal GameObject m_PlayerPanelGameObject;
    [SerializeField] internal float m_CursorSpeed;
    [SerializeField] internal Fighter m_SelectedFighter;
    [SerializeField] internal bool m_IsReady;

    [NonSerialized] private Vector2 i_Movement;
    [NonSerialized] private GameObject i_HoverGameObject;

    void Start() 
    {
        IsReady = false;
    }

    void Update() 
    {

        MoveCursor();
    }

    private void MoveCursor() 
    {
        Vector2 movement = new Vector2(i_Movement.x, i_Movement.y) * m_CursorSpeed * Time.deltaTime;
        m_PlayerCursorGameObject.transform.Translate(movement);
    }

    private void OnMove(InputValue value)
    {
        if (gameObject.active)
            i_Movement = value.Get<Vector2>();
    }

  

    // Select Fighter
    private void OnAButton(InputValue value)
    {
        if (gameObject.active) 
        {
            if (!m_IsReady)
            {
                if (i_HoverGameObject != null && i_HoverGameObject.name == "Fighter")
                    IsReady = true;
            }

            if (i_HoverGameObject != null && i_HoverGameObject.name == "ReadyInput")
            {
                GameObject playerManagerGameObject = GameObject.Find("PlayerManager");
                PlayerSelectionManager playerSelectionManager = playerManagerGameObject.GetComponent<PlayerSelectionManager>();
                playerSelectionManager.CheckPlayerSelectionPanelReady();
            }

            if (i_HoverGameObject != null && i_HoverGameObject.name == "BackInput") 
            {
                GameObject playerManagerGameObject = GameObject.Find("PlayerManager");
                PlayerSelectionManager playerSelectionManager = playerManagerGameObject.GetComponent<PlayerSelectionManager>();
                playerSelectionManager.ShowPlayerSelectionPanel();
            }

            if (i_HoverGameObject != null && i_HoverGameObject.tag == "UIButton")
            { 
                PlayerSelectionButton playerSelectionButton = i_HoverGameObject.GetComponent<PlayerSelectionButton>();
                playerSelectionButton.OnClickButton();


            }
            
        }
        
    }

    // Cancel previous action
    private void OnBButton(InputValue value)
    {
        if (gameObject.active) 
        {
            if (m_IsReady)
            {
                IsReady = false;

            }
        }
        
    }

    private void OnStart(InputValue value)
    {
        if (gameObject.active)
            Debug.Log("OnStart");
    }

    public GameObject PlayerCursorGameObject
    {
        get => m_PlayerCursorGameObject;
        set => m_PlayerCursorGameObject = value;
    }

    public GameObject PlayerPanelGameObject
    {
        get => m_PlayerPanelGameObject;
        set => m_PlayerPanelGameObject = value;
    }

    public bool IsReady
    {
        get => m_IsReady;
        set
        {
            GameObject selectionPanelGameObject = m_PlayerPanelGameObject.transform.Find("SelectionPanel").gameObject;
            GameObject readyGameObject = selectionPanelGameObject.transform.Find("ReadyLabel").gameObject;
            readyGameObject.SetActive(value);

            if (value)
                m_SelectedFighter = i_HoverGameObject.GetComponent<Fighter>();
            else
            {
                m_SelectedFighter = null;
                SetSelectedFighterName("");
            }


            m_IsReady = value;
        }
    }

    public Fighter SelectedFighter
    {
        get => m_SelectedFighter;
        set => m_SelectedFighter = value;
    }

    public void SetPlayerCursorIndexLabel(int index) 
    {
        GameObject playerIndexGameObject = m_PlayerCursorGameObject.transform.Find("PlayerIndexLabel").gameObject;
        TextMesh indexTextMesh = playerIndexGameObject.GetComponent<TextMesh>();
        indexTextMesh.text = index.ToString();
    }

    public void SetSelectedFighterName(string fighterName) 
    {
        GameObject selectionPanelGameObject = m_PlayerPanelGameObject.transform.Find("SelectionPanel").gameObject;
        GameObject selectedFighterPanelGameObject = selectionPanelGameObject.transform.Find("FighterSelectedPanel").gameObject;
        GameObject selectedFighterNameGameObject = selectedFighterPanelGameObject.transform.Find("FighterName").gameObject;
        TextMesh figherNameTextMesh = selectedFighterNameGameObject.GetComponent<TextMesh>();
        figherNameTextMesh.text = fighterName;
    }

    public void ShowPressStartPanel(bool show) 
    {
        if (m_PlayerPanelGameObject != null) 
        {
            GameObject selectionPanelGameObject = m_PlayerPanelGameObject.transform.Find("SelectionPanel").gameObject;
            selectionPanelGameObject.transform.Find("PressStartLabel").gameObject.SetActive(show);
            selectionPanelGameObject.transform.Find("FighterSelectedPanel").gameObject.SetActive(!show);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.name == "Fighter" && !m_IsReady) 
        {
            Fighter fighter = colliderGameObject.GetComponent<Fighter>();
            SetSelectedFighterName(fighter.FighterName);
        }
        i_HoverGameObject = colliderGameObject;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.name == "Fighter" && !m_IsReady)
        {
            Fighter fighter = colliderGameObject.GetComponent<Fighter>();
            SetSelectedFighterName(""); 
        }
        i_HoverGameObject = null;
    }
}
