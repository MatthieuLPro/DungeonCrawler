using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerSelectionPanelManager : MonoBehaviour
{
    [SerializeField] internal enumSelectionModePanel m_SelectionModePanel;
    [SerializeField] internal GameObject m_SceneManagerObject; 
    [SerializeField] internal GameObject m_ReadyButtonGameObject;
    [SerializeField] internal List<GameObject> m_PlayerGameObjectList;

    public enumSelectionModePanel SelectionModePanel
    {
        get { return m_SelectionModePanel; }
        set { m_SelectionModePanel = value; }
    }

    public GameObject SceneManagerObject
    {
        get { return m_SceneManagerObject; }
        set { m_SceneManagerObject = value; }
    }

    public GameObject ReadyButtonGameObject
    {
        get { return m_ReadyButtonGameObject; }
        set { m_ReadyButtonGameObject = value; }
    }

    public void OnNewPlayerJoin(int index, PlayerCursor playerCursor) 
    {
        GameObject playerPanelGameObject = m_PlayerGameObjectList[index];
        PlayerPanel playerPanel = playerPanelGameObject.GetComponent<PlayerPanel>();
        playerPanel.Playerjoin();
        playerCursor.PlayerPanelGameObject = playerPanelGameObject;
    }

    public void OnReadyButton()
    {
        if (_CheckPlayerSelectionPanelReady())
        {
            _GetPlayerSelectionManager().ShowGameSelectionPanel();
        }
        else
        {
            Debug.Log("Player not Ready");
        }
    }

    public void ShowPanel(bool show)
    {
        gameObject.SetActive(show);
    }

    private bool _CheckPlayerSelectionPanelReady() 
    {
        List<GameObject> cursorGameObjectList = _GetPlayerSelectionManager().CursorGameObjectList;
        foreach(GameObject cursorGameObject in cursorGameObjectList)
        {
            PlayerCursor cursor = cursorGameObject.GetComponent<PlayerCursor>();
            if (!cursor.GetPlayerPanel().Ready)
                return false;
        }
        return true;
    }

    private PlayerSelectionManager _GetPlayerSelectionManager() 
    {
        return SceneManagerObject.GetComponent<PlayerSelectionManager>();
    }
}
