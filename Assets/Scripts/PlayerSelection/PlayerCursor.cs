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
    [SerializeField] internal float m_CursorSpeed;
    [SerializeField] internal Fighter m_SelectedFighter;

    [SerializeField] internal bool m_IsReady;

    [SerializeField] internal GameObject m_PlayerCursorGameObject;
    [SerializeField] internal GameObject m_PlayerPanelGameObject;
    [SerializeField] internal GameObject m_SceneManagerGameObject;

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
        if (gameObject.active && i_HoverGameObject != null) 
        {
            // Behavior in Player Selection panel mode
            PlayerSelectionManager playerSelectionManager = m_SceneManagerGameObject.GetComponent<PlayerSelectionManager>();
            if (playerSelectionManager.CurrentSelectionModePanel == enumSelectionModePanel.playerSelectionPanel) 
            {
                // On Click Fighter Prefab
                PlayerPanel playerPanel = GetPlayerPanel();
                    
                if (!playerPanel.Ready && i_HoverGameObject.name == "Fighter")
                {
                    Fighter fighter = i_HoverGameObject.GetComponent<Fighter>();
                    playerPanel.SetFighterName(fighter.FighterName);
                    playerPanel.Ready = true;
                }

                // On click Ready Button
                if (i_HoverGameObject.name == "ReadyInput" && playerPanel.Ready) 
                {
                    GameObject playerSelectionPanelGameObject = playerSelectionManager.PlayerSelectionPanelGameObject;
                    PlayerSelectionPanelManager playerSelectionPanelManager = playerSelectionPanelGameObject.GetComponent<PlayerSelectionPanelManager>();
                    playerSelectionPanelManager.OnReadyButton();
                }
            }
            else if(playerSelectionManager.CurrentSelectionModePanel == enumSelectionModePanel.gameModePanel)
            {
                // On click Back Button
                if (i_HoverGameObject.name == "BackInput") 
                {
                    GameObject gameModeSelectionPanelGameObject = playerSelectionManager.GameSelectionPanelGameObject;
                    GameModePanelManager gameModePanelManager = gameModeSelectionPanelGameObject.GetComponent<GameModePanelManager>();
                    gameModePanelManager.OnBackButton();
                }

                // On click Start Button
                if (i_HoverGameObject.name == "StartRaceInput")
                {
                    GameObject gameModeSelectionPanelGameObject = playerSelectionManager.GameSelectionPanelGameObject;
                    GameModePanelManager gameModePanelManager = gameModeSelectionPanelGameObject.GetComponent<GameModePanelManager>();
                    gameModePanelManager.OnStartButton();
                }

                // On click Option Button
                if(i_HoverGameObject.tag == "OptionButton") 
                {
                    PlayerSelectionButton optionButton = i_HoverGameObject.GetComponent<PlayerSelectionButton>();
                    optionButton.OnClickButton();
                    Debug.Log("Click UI BUtton");
                }
            }


            /**/

            /**/

            /*  playerSelectionManager.CheckPlayerSelectionPanelReady();
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
            }*/

            /*if (i_HoverGameObject != null && i_HoverGameObject.name == "StartRaceInput")
            {
                GameObject gameModeSelectionGameObject = i_HoverGameObject.Find("GameModeSelectionPanel");

            }*/
        }

    }

    // Cancel previous action
    private void OnBButton(InputValue value)
    {
        if (gameObject.active) 
        {
            // Behavior in Player Selection panel mode
            PlayerSelectionManager playerSelectionManager = m_SceneManagerGameObject.GetComponent<PlayerSelectionManager>();

            if (playerSelectionManager.CurrentSelectionModePanel == enumSelectionModePanel.playerSelectionPanel) 
            {
                if (GetPlayerPanel().Ready)
                    GetPlayerPanel().Ready = false;
            }

            if (playerSelectionManager.CurrentSelectionModePanel == enumSelectionModePanel.gameModePanel)
            {
                playerSelectionManager.ShowPlayerSelectionPanel();
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

    public GameObject SceneManagerGameObject
    {
        get => m_SceneManagerGameObject;
        set => m_SceneManagerGameObject = value;
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
                GetPlayerPanel().SetFighterName("");
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        PlayerSelectionManager playerSelectionManager = m_SceneManagerGameObject.GetComponent<PlayerSelectionManager>();
        if (playerSelectionManager.CurrentSelectionModePanel == enumSelectionModePanel.playerSelectionPanel) 
        {
            PlayerPanel playerPanel = m_PlayerPanelGameObject.GetComponent<PlayerPanel>();
            if (colliderGameObject.name == "Fighter" && !GetPlayerPanel().Ready)
            {
                Fighter fighter = colliderGameObject.GetComponent<Fighter>();
                GetPlayerPanel().SetFighterName(fighter.FighterName);
            }
        }
        
        i_HoverGameObject = colliderGameObject;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        PlayerSelectionManager playerSelectionManager = m_SceneManagerGameObject.GetComponent<PlayerSelectionManager>();
        if (playerSelectionManager.CurrentSelectionModePanel == enumSelectionModePanel.playerSelectionPanel) 
        {
            if (colliderGameObject.name == "Fighter" && !GetPlayerPanel().Ready)
            {
                GetPlayerPanel().SetFighterName("");
            }
        }
            
        i_HoverGameObject = null;
    }

    public PlayerPanel GetPlayerPanel() 
    {
        return m_PlayerPanelGameObject.GetComponent<PlayerPanel>();
    }
}
