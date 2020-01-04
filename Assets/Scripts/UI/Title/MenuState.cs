using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : MonoBehaviour
{
    public MainMenuStates Type;
    public List<UIInput> UIInputsList;
    public MainMenuManager Manager;
    public UIInput CurrentSelectedUIInput;

    private GameObject _MenuStateGO;
    private int _DefaulFocusInputIndex;
    private int _CurrentFocusInputIndex;
    private int _PreviousFocusInputIndex;
    private bool _CurrentIsAxisInUseState = false;

    void Start()
    {
        _MenuStateGO = gameObject;
        _DefaulFocusInputIndex = 0;

        // Get Inputs
       foreach (Transform child in transform)
        {
            if (child.tag == "UIInput")
                UIInputsList.Add(child.GetComponent<UIInput>());
        }

        _FocusInput(_DefaulFocusInputIndex);
    }

    void Update()
    {
        // Check Players input
        bool IsAxisIsUse = InputManager.IsAxisInUse();

        if (InputManager.MainVertical() > 0)
        {
            if (_CurrentIsAxisInUseState == false)
            {
                FocusPreviousInput();
                _CurrentIsAxisInUseState = true;
            }
        }
        else if (InputManager.MainVertical() < 0)
        {
            if (_CurrentIsAxisInUseState == false)
            {
                FocusNextInput();
                _CurrentIsAxisInUseState = true;
            }
        }
        else
            _CurrentIsAxisInUseState = false;

        if (InputManager.XButton())
            ActivateFocusInput();


        if (InputManager.BButton())
            Manager.SelectPreviousMenu();
    }

    // Public Functions

    public void FocusNextInput() 
    {
        int lastIndex = (UIInputsList.Count - 1);
        int nextUIInputIndex = (_CurrentFocusInputIndex == lastIndex) ? 0 : _CurrentFocusInputIndex + 1;
        _FocusInput(nextUIInputIndex);
    }

    public void FocusPreviousInput() 
    {
        int lastIndex = (UIInputsList.Count - 1);
        int previousUIInputIndex = (_CurrentFocusInputIndex == 0) ? lastIndex : _CurrentFocusInputIndex - 1;
        _FocusInput(previousUIInputIndex);
    }

    public void ActivateFocusInput() 
    {
        UIInput FocusInput = UIInputsList[_CurrentFocusInputIndex];
        FocusInput.Activate();
    }

    // Private Functions

    private void _FocusInput(int index) 
    {
        if (UIInputsList.Count - 1 >= index) 
        {
            UIInput NewFocusInput = UIInputsList[index];
            if (NewFocusInput != null)
            {
                // Check previous Input
                UIInput CurrentFocuUIInput = UIInputsList[_CurrentFocusInputIndex];
                if (CurrentFocuUIInput != null)
                {
                    CurrentFocuUIInput.SetFocus(false);
                    _PreviousFocusInputIndex = _CurrentFocusInputIndex;
                }

                UIInput NewSelectedUIInput = UIInputsList[index];
                NewSelectedUIInput.SetFocus(true);

                _CurrentFocusInputIndex = index;
                CurrentSelectedUIInput = NewSelectedUIInput;
            }
        }
    }

    public void ShowMenuPanel(bool isShow) 
    {
        _MenuStateGO.SetActive(isShow);
    }

    public bool IsActive() 
    {
        return _MenuStateGO.activeSelf;
    }

    public MainMenuManager GetManager() 
    {
        return Manager;
    }
}
