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
    [SerializeField] internal List<GameObject> m_PlayerCursorList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPlayerJoined(PlayerInput playerCursorInput)
    {
        GameObject playerCursorPrefab = playerCursorInput.gameObject;
        playerCursorPrefab.name = "PlayerCursor_" + playerCursorInput.playerIndex;
        m_PlayerCursorList.Add(playerCursorPrefab);

        GameObject playerPanelGameObject = m_PlayerSelectionPanelGameObject.transform.Find("PlayerPanel_" + playerCursorInput.playerIndex).gameObject;

        PlayerCursor playerCursor = playerCursorPrefab.GetComponent<PlayerCursor>();
        playerCursor.PlayerPanelGameObject = playerPanelGameObject;
        playerCursor.SetPlayerCursorIndexLabel(playerCursorInput.playerIndex + 1);
        playerCursor.ShowPressStartPanel(false);


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

    public void ShowPlayerSelectionPanel() 
    {
        m_PlayerSelectionPanelGameObject.SetActive(true);
        m_GameSelectionPanelGameObject.SetActive(false);

        foreach (GameObject playerCursorGameObject in m_PlayerCursorList)
        {
            playerCursorGameObject.SetActive(true);
        }

        m_PlayerInputManager.EnableJoining();
    }

    public void ShowGameSelectionPanel()
    {
        m_GameSelectionPanelGameObject.SetActive(true);
        m_PlayerSelectionPanelGameObject.SetActive(false);

        foreach (GameObject playerCursorGameObject in m_PlayerCursorList)
        {
            if (playerCursorGameObject.name != "PlayerCursor_0")
                playerCursorGameObject.SetActive(false);
        }

        m_PlayerInputManager.DisableJoining();
    }

    public void CheckPlayerSelectionPanelReady() 
    {
        bool isReady = true;
        foreach (GameObject playerCursorGameObject in m_PlayerCursorList)
        {
            PlayerCursor playerCursor = playerCursorGameObject.GetComponent<PlayerCursor>();
            if (!playerCursor.IsReady)
                isReady = false;
        }

        if (isReady) 
        {
            ShowGameSelectionPanel();
        }
            
    }
}
