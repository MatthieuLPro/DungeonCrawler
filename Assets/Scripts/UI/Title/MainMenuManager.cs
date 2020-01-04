using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    public List<MenuState> MenuStatesList;
    public MainMenuStates DefaultState;

    private MainMenuStates _CurrentMainMenuState;
    private MainMenuStates _PreviousMainMenuState;
   

    void Start()
    {
        // Initialize Panels
        foreach (Transform child in transform)
        {
            if (child.tag == "UIPanel")
                MenuStatesList.Add(child.GetComponent<MenuState>());
        }

        _CloseCurrentActiveMainMenu();
        _SelectMenu(DefaultState);
    }

    // Public Functions

    public void SelectMenu(MainMenuStates SelectedType) 
    {
        _SelectMenu(SelectedType);
    }

    public void SelectPreviousMenu() 
    {
        MainMenuStates PreviousMenuState = MainMenuStates.EmptyState;
        switch (_CurrentMainMenuState)
        {
            case MainMenuStates.StartGameState:
                PreviousMenuState = MainMenuStates.EmptyState;
                break;
            case MainMenuStates.OptionsState:
                PreviousMenuState = MainMenuStates.StartGameState;
                break;
            case MainMenuStates.ExitGameState:
                PreviousMenuState = MainMenuStates.StartGameState;
                break;
            case MainMenuStates.PlayersSelectionState:
                PreviousMenuState = MainMenuStates.StartGameState;
                break;
            case MainMenuStates.SelectGameTypeState:
                PreviousMenuState = MainMenuStates.PlayersSelectionState;
                break;
            case MainMenuStates.EmptyState:
                PreviousMenuState = MainMenuStates.EmptyState;
                break;
        }

        _SelectMenu(PreviousMenuState);
    }

    public void DisabledCurrentMenu() { }

    // Private Functions

    private void _SelectMenu(MainMenuStates SelectedType) 
    {
        MenuState NewSelectedMenuState = _GetMenuStateByType(SelectedType);
        if (NewSelectedMenuState != null) 
        {
            MenuState CurrentSelectedMenuState = _GetMenuStateByType(_CurrentMainMenuState);
            if (CurrentSelectedMenuState != null)
                CurrentSelectedMenuState.ShowMenuPanel(false);
            NewSelectedMenuState.ShowMenuPanel(true);

            _PreviousMainMenuState = _CurrentMainMenuState;
            _CurrentMainMenuState = SelectedType;
        }
    }

    private MenuState _GetMenuStateByType(MainMenuStates SearchType) 
    {
        MenuState SearchMenuState = null;
        foreach (MenuState FetchMenuState in MenuStatesList)
        {
            if (FetchMenuState.Type == SearchType)
                SearchMenuState = FetchMenuState;
        }

        return SearchMenuState;
    }

    private void _CloseCurrentActiveMainMenu() 
    {
        foreach (MenuState FetchMenuState in MenuStatesList)
        {
            if (FetchMenuState.IsActive())
                FetchMenuState.ShowMenuPanel(false);
        }
    }
}
