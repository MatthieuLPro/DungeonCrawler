using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] internal bool m_Ready;
    [SerializeField] internal GameObject m_PressStartGameObject;
    [SerializeField] internal GameObject m_ReadyLabelGameObject;
    [SerializeField] internal GameObject m_FighterSelectedGameObject;

    void Start() 
    {
        PlayerLeave();
    }

    public bool Ready
    {
        get => m_Ready;
        set
        {
            m_ReadyLabelGameObject.SetActive(value);
            m_Ready = value;
            if (!value)
                SetFighterName("");
        }
    }

    public GameObject PressStartGameObject
    {
        get { return m_PressStartGameObject; }
        set { m_PressStartGameObject = value; }
    }

    public GameObject ReadyLabelGameObject
    {
        get { return m_ReadyLabelGameObject; }
        set { m_ReadyLabelGameObject = value; }
    }

    public GameObject FighterSelectedGameObject
    {
        get { return m_FighterSelectedGameObject; }
        set { m_FighterSelectedGameObject = value; }
    }

    public void Playerjoin() 
    {
        m_FighterSelectedGameObject.SetActive(true);
        m_PressStartGameObject.SetActive(false);
        m_ReadyLabelGameObject.SetActive(false);
        SetFighterName("");
    }

    public void PlayerLeave() 
    {
        m_PressStartGameObject.SetActive(true);
        m_ReadyLabelGameObject.SetActive(false);
        m_FighterSelectedGameObject.SetActive(false);
    }

    public void SetFighterName(string name) 
    {
        GameObject fighterNameGameObject = m_FighterSelectedGameObject.transform.Find("FighterName").gameObject;
        TextMesh fighterNameTextMesh = fighterNameGameObject.GetComponent<TextMesh>();
        fighterNameTextMesh.text = name;
    }

    
}
