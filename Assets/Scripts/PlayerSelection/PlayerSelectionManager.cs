using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerSelectionManager : MonoBehaviour
{
    [SerializeField] internal PlayerInputManager m_PlayerInputManager;
    [SerializeField] internal GameObject m_PlayerSelectionPanelGameObject;
    [SerializeField] internal GameObject m_GameSelectionPanelGameObject;
    [SerializeField] internal List<GameObject> m_CursorList;
    [SerializeField] internal List<PlayerParameter> m_PlayerParameterList;
    [SerializeField] internal enumSelectionModePanel m_CurrentSelectionModePanel;

    void Start() 
    {
        ShowPlayerSelectionPanel();
    }

    /// <summary>
    /// Event call when a new player joined the session
    /// </summary>
    /// <remarks>
    /// Create a cursor prefab and initialize a new player in the playerSelectionPanel
    /// </remarks>
    private void OnPlayerJoined(PlayerInput newPlayerInput)
    {
        LevelParameters levelParameters = gameObject.GetComponent<LevelParameters>();

        GameObject cursorPrefab = newPlayerInput.gameObject;
        cursorPrefab.name = "PlayerCursor_" + newPlayerInput.playerIndex;
        m_CursorList.Add(cursorPrefab);

        PlayerParameter playerParameter = gameObject.AddComponent<PlayerParameter>();
        playerParameter.Index = newPlayerInput.playerIndex;
        playerParameter.PlayerInputDevice = newPlayerInput;
        m_PlayerParameterList.Add(playerParameter);

        levelParameters.PlayerParameterList.Add(playerParameter);

        PlayerCursor cursor = cursorPrefab.GetComponent<PlayerCursor>();
        cursor.SceneManagerGameObject = gameObject;

        PlayerSelectionPanelManager playerSelectionPanelManager = m_PlayerSelectionPanelGameObject.GetComponent<PlayerSelectionPanelManager>();
        playerSelectionPanelManager.OnNewPlayerJoin(newPlayerInput.playerIndex, cursorPrefab.GetComponent<PlayerCursor>());
    }

    public enumSelectionModePanel CurrentSelectionModePanel
    {
        get => m_CurrentSelectionModePanel;
        set => m_CurrentSelectionModePanel = value;
    }

    public PlayerInputManager PlayerInputManagerObj
    {
        get => m_PlayerInputManager;
        set => m_PlayerInputManager = value;
    }

    public GameObject PlayerSelectionPanelGameObject
    {
        get => m_PlayerSelectionPanelGameObject;
        set => m_PlayerSelectionPanelGameObject = value;
    }

    public GameObject GameSelectionPanelGameObject
    {
        get => m_GameSelectionPanelGameObject;
        set => m_GameSelectionPanelGameObject = value;
    }

    public List<GameObject> CursorGameObjectList 
    {
        get => m_CursorList;
    }

    /// <summary>
    /// Show the players selection panel
    /// </summary>
    /// <remarks>
    /// Set active on all the current player cursor create
    /// </remarks>
    public void ShowPlayerSelectionPanel() 
    {
        m_PlayerSelectionPanelGameObject.SetActive(true);
        m_GameSelectionPanelGameObject.SetActive(false);

        foreach (GameObject cursorGameObject in m_CursorList)
        {
            cursorGameObject.SetActive(true);
        }

        m_PlayerInputManager.EnableJoining();
        PlayerSelectionPanelManager playerSelectionPanelManager = m_PlayerSelectionPanelGameObject.GetComponent<PlayerSelectionPanelManager>();
        m_CurrentSelectionModePanel = playerSelectionPanelManager.SelectionModePanel;
    }

    /// <summary>
    /// Show the Game selection panel
    /// </summary>
    /// <remarks>
    /// Set active only on the first player cursor
    /// </remarks>
    public void ShowGameSelectionPanel()
    {
        m_GameSelectionPanelGameObject.SetActive(true);
        m_PlayerSelectionPanelGameObject.SetActive(false);

        foreach (GameObject cursorGameObject in m_CursorList)
        {
            if (cursorGameObject.name != "PlayerCursor_0")
                cursorGameObject.SetActive(false);
        }

        m_PlayerInputManager.DisableJoining();
        GameModePanelManager gameModePanelManager = m_GameSelectionPanelGameObject.GetComponent<GameModePanelManager>();
        m_CurrentSelectionModePanel = gameModePanelManager.SelectionModePanel;

        gameModePanelManager.Initialize();
    }

    public void CheckPlayerSelectionPanelReady() 
    {
        bool isReady = true;
        foreach (GameObject cursorGameObject in m_CursorList)
        {
            PlayerCursor cursor = cursorGameObject.GetComponent<PlayerCursor>();
            if (!cursor.IsReady)
                isReady = false;
        }

        if (isReady) 
        {
            ShowGameSelectionPanel();
        }
    }


}
