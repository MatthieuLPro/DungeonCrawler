using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugRoomScript : MonoBehaviour
{
    public List<string> LevelsNameList = new List<string>();

    public GameObject PlayersDropdownGO;
    public GameObject LevelsDropdownGO;

    public RaceParameters raceParameters;

    private int _PlayersNumber;
    private string _LevelName;
    void Start()
    {
        _initializeDungeonsPanel();
    }



    public void HandleValidateSelection()
    {
        Dropdown PlayersNumberDropdown = PlayersDropdownGO.GetComponent<Dropdown>();
        Dropdown LevelsDropdown = LevelsDropdownGO.GetComponent<Dropdown>();

        _PlayersNumber = PlayersNumberDropdown.value + 1;
        _LevelName = LevelsNameList[LevelsDropdown.value];

        Debug.Log("Go to scene: " + _LevelName + " With: " + _PlayersNumber);
        raceParameters.SetPlayersNumber(_PlayersNumber);
        SceneManager.LoadScene(_LevelName);
        Destroy(gameObject);
    }

    private void _initializeDungeonsPanel() 
    {
        Dropdown LevelsDropdown = LevelsDropdownGO.GetComponent<Dropdown>();
        LevelsDropdown.ClearOptions();
        LevelsDropdown.AddOptions(LevelsNameList);    
    }
}
