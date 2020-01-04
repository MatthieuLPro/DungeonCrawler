using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonInput : UIInput
{
    public MainMenuStates BindMainMenuState;

    public override void InputValidation()
    {
        MainMenuManager Manager = MenuState.GetManager();
        Manager.SelectMenu(BindMainMenuState);
    }

    /*public override void Select(bool _isSelect)
        {
            IsSelect = _isSelect;
            Color32 newColor = _isSelect ? _SelectedColor : _UnselectedColor;
            GetInput().GetComponent<Image>().color = newColor;
            Source.PlayOneShot(OnSelectSound);
        }

        public override void ActivateInput() 
        {
            if (BindPanel != null) 
            {
                GETUIPanel().ShowUIPanel(BindPanel);
                //BindPanel.SetActive(true);
            }


        }*/
}
