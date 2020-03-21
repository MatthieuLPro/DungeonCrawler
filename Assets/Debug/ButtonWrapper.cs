using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonWrapper : MonoBehaviour
{
    [SerializeField] GameObject ButtonWrapperGO;
    private GameObject buttonGO;

    // Start is called before the first frame update
    void Start()
    {
        buttonGO = ButtonWrapperGO.transform.Find("Button").gameObject;
    }

    public void SetLabel(string label) 
    {
        Debug.Log(buttonGO.transform.Find("Text"));
       //buttonGO.transform.Find("Text").gameObject.GetComponent<Text>() = label;
    }
}
