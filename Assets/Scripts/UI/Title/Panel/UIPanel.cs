using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    /*private GameObject _Panel;


    private int _DefaultSelectedIndex = 0;
    
    [SerializeField]
    private int _currentSelectedIndex;

    [SerializeField]
    private List<GameObject> _UIInputsList = new List<GameObject>();

    public UITitlePageManager PageManager;

    public virtual void HandleInputs() {}

    private void _InitializeUIPanel() 
    {
        Transform panelTransform = _Panel.GetComponent<Transform>();
        foreach (Transform childGameObject in panelTransform)
        {
            if (childGameObject.tag == "UIInput")
                _UIInputsList.Add(childGameObject.gameObject);
        }
        SelectButton(_DefaultSelectedIndex);
    }

    void Start() 
    {
        _Panel = gameObject;
        PageManager = PageManager.GetComponent<UITitlePageManager>();
        _InitializeUIPanel();
    }

    void Update() 
    {
        HandleInputs();
    }

    public void Show(bool _isShow) 
    {
        _Panel.SetActive(_isShow);
    }

    public void SelectButton(int _index) 
    {
        if (_UIInputsList[_index] != null) 
        {
            GameObject currentSelectedInput = _UIInputsList[_currentSelectedIndex];
            GameObject newSelectedUIInput = _UIInputsList[_index];
            currentSelectedInput.GetComponent<UIInput>().Select(false);
            newSelectedUIInput.GetComponent<UIInput>().Select(true);
            _currentSelectedIndex = _index;
        }
    }

    public void SelectNextButton() 
    {
        Debug.Log(_UIInputsList.Count);
        int nextUIButtonIndex = (_currentSelectedIndex < (_UIInputsList.Count - 1)) ? _currentSelectedIndex + 1 : 0;
        SelectButton(nextUIButtonIndex);
    }

    public void SelectPreviousButton()
    {
        int previousUIButtonIndex = (_currentSelectedIndex > 0) ? _currentSelectedIndex - 1 : (_UIInputsList.Count - 1);
        SelectButton(previousUIButtonIndex);
    }

    public void ActivateSelectedInput() 
    {
        GameObject currentSelectedInput = _UIInputsList[_currentSelectedIndex];
        currentSelectedInput.GetComponent<UIInput>().ActivateInput();
    }

    public void Cancel() 
    {
        PageManager.DisplayPreviousUIPageComponent();
    }

    public void ShowUIPanel(GameObject _showUIPanelGO) 
    {
        PageManager.DisplayUIPageComponent(_showUIPanelGO);
    }*/
}
