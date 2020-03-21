using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Debug_room_manager : MonoBehaviour
{
    public List<string> DungeonNameList = new List<string>();

    public string SelectedLevelName;
    public int SelectedPlayersCount;

    public Dropdown PlayersDropdown;
    public Dropdown DungeonDropdown;

    public RaceParameters raceParameters;

    [SerializeField] GameObject PlayerCountSelectionPanel;
    [SerializeField] SelectionPanel DungeonSelectionPanel;

    [SerializeField] GameObject ButtonWrapperPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //DungeonDropdown.AddOptions(LevelNameArray);
        _initializeDungeonsPanel();
    }

    // Update is called once per frame
    void Update() {

        //HandlePlayersCountDropdown(PlayersDropdown.value);
        //HandleDungeonDropdown(DungeonDropdown.value);
    }

    public void HandlePlayersCountDropdown(int indexValue)
    {
        //SelectedPlayersCount = indexValue + 1;
    }

    public void HandleDungeonDropdown(int indexValue) 
    {
        //SelectedLevelName = LevelNameArray[indexValue];
    }

    public void HandleValidateSelection()
    {
        raceParameters.SetPlayersCount(SelectedPlayersCount);
        SceneManager.LoadScene(SelectedLevelName);
    }

    private void _initializeDungeonsPanel() 
    {
        foreach (string dungeonName in DungeonNameList) 
        {
           /* GameObject dungeonButtonWrapper = Instantiate(ButtonWrapperPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            Transform buttonTextTransform = dungeonButtonWrapper.transform.Find("Button").transform.Find("Text");
            buttonTextTransform.GetComponent<Text>().text = dungeonName;*/

            //dungeonButtonWrapper.transform.parent = DungeonSelectionPanel.transform;
        }
            //Debug.Log(levelName);
    }

    private void _getButtonWrapper(string label)
    {
        //GameObject go = Instantiate(A, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }
}
