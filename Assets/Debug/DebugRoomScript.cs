using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugRoomScript : MonoBehaviour
{
    public List<string> LevelsNameList = new List<string>();

    public GameObject GameOptionsPanel;
    public GameObject LevelsDropdownGO;

    public GameParameters gameParameters;

    private int _playersNumber = 1;
    private int _playersSpeed = 0;
    private int _consumablePresence = 0;
    private int _staticMonsterPresence = 0;
    private string _LevelName;
    void Start()
    {
        _initializeDungeonsPanel();
    }

    public void HandleValidateSelection()
    {
        Transform gameOptionsWrapper       = GameOptionsPanel.transform.Find("body").GetChild(0).gameObject.transform;

        Dropdown PlayersNumberDropdown      = gameOptionsWrapper.Find("PlayerNumber").GetChild(1).GetComponent<Dropdown>();
        Dropdown PlayersSpeedDropdown       = gameOptionsWrapper.Find("PlayerSpeed").GetChild(1).GetComponent<Dropdown>();
        Dropdown ConsumableObjectDropdown   = gameOptionsWrapper.Find("ConsumableObject").GetChild(1).GetComponent<Dropdown>();
        Dropdown StaticMonsterDropdown      = gameOptionsWrapper.Find("StaticMonster").GetChild(1).GetComponent<Dropdown>();

        Dropdown LevelsDropdown             = LevelsDropdownGO.GetComponent<Dropdown>();

        PlayersNumber           = PlayersNumberDropdown.value + 1;
        PlayersSpeed            = PlayersSpeedDropdown.value;
        ConsumablePresence      = ConsumableObjectDropdown.value;
        StaticMonsterPresence   = StaticMonsterDropdown.value;
        _LevelName = LevelsNameList[LevelsDropdown.value];

        Debug.Log("Go to scene: " + _LevelName + " With: " + _playersNumber);
        gameParameters.SetGameParameters(PlayersNumber, PlayersSpeed, ConsumablePresence, StaticMonsterPresence);
        SceneManager.LoadScene(_LevelName);
        Destroy(gameObject);
    }

    private void _initializeDungeonsPanel() 
    {
        Dropdown LevelsDropdown = LevelsDropdownGO.GetComponent<Dropdown>();
        LevelsDropdown.ClearOptions();
        LevelsDropdown.AddOptions(LevelsNameList);    
    }

    /* ************************************************ */
    /* Getters & Setters */
    /* ************************************************ */
    public int PlayersNumber {
        get { return _playersNumber; }
        set { _playersNumber = value; }
    }

    public int PlayersSpeed {
        get { return _playersSpeed; }
        set { _playersSpeed = value; }
    }

    public int ConsumablePresence {
        get { return _consumablePresence; }
        set { _consumablePresence = value; }
    }

    public int StaticMonsterPresence {
        get { return _staticMonsterPresence; }
        set { _staticMonsterPresence = value; }
    }

}
