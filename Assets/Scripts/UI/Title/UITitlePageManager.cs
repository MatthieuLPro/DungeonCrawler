using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitlePageManager : MonoBehaviour
{
    public GameObject DefaultDisplayUIPageComponent;
    public GameObject CurrentUIPageComponent;

    public GameObject _PreviousUIPageComponent;

    //public List<GameObject> UIPageComponentList;

    // Start is called before the first frame update
    void Start()
    {
        DisplayUIPageComponent(DefaultDisplayUIPageComponent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayUIPageComponent(GameObject _showUIPageComponent) 
    {
        if (CurrentUIPageComponent)
            CurrentUIPageComponent.SetActive(false);
        _PreviousUIPageComponent = CurrentUIPageComponent;
        CurrentUIPageComponent = _showUIPageComponent;
        CurrentUIPageComponent.SetActive(true);
    }

    public void DisplayPreviousUIPageComponent() 
    {
        if (_PreviousUIPageComponent != null)
            DisplayUIPageComponent(_PreviousUIPageComponent);
    }
}
