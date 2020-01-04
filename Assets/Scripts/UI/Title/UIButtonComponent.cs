using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonComponent : MonoBehaviour
{
    public GameObject PageCanvas;
    public GameObject UIButton;
    public GameObject ParentUIPanel;
    public GameObject LinkedUIPanel;


    // Start is called before the first frame update
    void Start()
    {

        UIButton = gameObject;
    }

    public void SwitchUIPanel() 
    {
        UITitlePageManager test = PageCanvas.GetComponent<UITitlePageManager>();
        //test.DisplayUIPageComponent(LinkedUIPanel);
        //PageManager.DisplayUIPageComponent(LinkedUIPanel);
    }

    public void Select() 
    {

    }
}
