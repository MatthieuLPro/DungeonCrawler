using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameModePanelManager : MonoBehaviour
{
    [SerializeField] internal enumSelectionModePanel m_SelectionModePanel;
    [SerializeField] internal GameObject m_BackButtonGameObject;
    [SerializeField] internal GameObject m_StartButtonGameObject;
    [SerializeField] internal GameObject m_SceneManagerObject;

    [SerializeField] internal string m_LevelName;
    [SerializeField] internal int m_ConsumableInValue;
    [SerializeField] internal int m_StaticMonsterValue;

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

    public GameObject BackButtonGameObject
    {
        get { return m_BackButtonGameObject; }
        set { m_BackButtonGameObject = value; }
    }

    public GameObject StartButtonGameObject
    {
        get { return m_StartButtonGameObject; }
        set { m_StartButtonGameObject = value; }
    }

    // Options Panels
    [SerializeField] internal GameObject m_LevelsGameObject;
    [SerializeField] internal GameObject m_ConsumableInGameObject;
    [SerializeField] internal GameObject m_StaticMonsterGameObject;


    public GameObject LevelsGameObject
    {
        get { return m_LevelsGameObject; }
        set { m_LevelsGameObject = value; }
    }

    public GameObject ConsumableInGameObject
    {
        get { return m_ConsumableInGameObject; }
        set { m_ConsumableInGameObject = value; }
    }

    public GameObject StaticMonsterGameObject
    {
        get { return m_StaticMonsterGameObject; }
        set { m_StaticMonsterGameObject = value; }
    }

    public string GetOptionPanelValue(GameObject optionPanelGameObject) 
    {
        OptionPanel optionPanel = optionPanelGameObject.GetComponent<OptionPanel>();
        PlayerSelectionButton selectedButton = optionPanel.GetSelectedButton();
        return selectedButton.Value;
    }

    public void CheckGameModePanel() 
    {
        LevelParameters levelParameters = m_SceneManagerObject.GetComponent<LevelParameters>();
        levelParameters.LevelName = GetOptionPanelValue(m_LevelsGameObject);
        levelParameters.ConsumableIn = int.Parse(GetOptionPanelValue(m_ConsumableInGameObject));
        levelParameters.StaticMonsters = int.Parse(GetOptionPanelValue(m_StaticMonsterGameObject));
    }

    public void OnBackButton() 
    {
        _GetPlayerSelectionManager().ShowPlayerSelectionPanel();
    }

    public void OnStartButton() 
    {
        // Fetch all data
        CheckGameModePanel();
    }

    public void Initialize() 
    {
        OptionPanel levelsGameOptionPanel = m_LevelsGameObject.GetComponent<OptionPanel>();
        PlayerSelectionButton currentLevelOptionButton = levelsGameOptionPanel.GetSelectedButton();
        currentLevelOptionButton.OnClickButton();

        OptionPanel consumableInGameOptionPanel = m_ConsumableInGameObject.GetComponent<OptionPanel>();
        PlayerSelectionButton currentConsumableInOptionButton = consumableInGameOptionPanel.GetSelectedButton();
        currentConsumableInOptionButton.OnClickButton();

        OptionPanel staticMonsterGameOptionPanel = m_StaticMonsterGameObject.GetComponent<OptionPanel>();
        PlayerSelectionButton currentStaticMonsterOptionButton = staticMonsterGameOptionPanel.GetSelectedButton();
        currentStaticMonsterOptionButton.OnClickButton();
    }

    private PlayerSelectionManager _GetPlayerSelectionManager()
    {
        return SceneManagerObject.GetComponent<PlayerSelectionManager>();
    }

}
