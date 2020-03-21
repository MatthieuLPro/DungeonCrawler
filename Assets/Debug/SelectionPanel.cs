using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    public List<string> ElementList = new List<string>();

    GameObject SelectionPanelGO;
    [SerializeField] GameObject ButtonWrapperPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddElementToList(string newElement) 
    {
        ElementList.Add(newElement);

    }
}
