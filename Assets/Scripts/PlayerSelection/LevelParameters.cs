using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class LevelParameters : MonoBehaviour
{
    [SerializeField] static public int m_PlayerCount;
    [SerializeField] static public List<PlayerParameter> m_PlayerParameterList;

    public static int PlayerCount
    {
        get { return m_PlayerCount; }
        set { m_PlayerCount = value; }
    }

    public static List<PlayerParameter> PlayerParameterList
    {
        get { return m_PlayerParameterList; }
        set { m_PlayerParameterList = value; }
    }

    // Level Properties
    [SerializeField] static public string m_LevelName;
    [SerializeField] static public int m_ConsumableIn;
    [SerializeField] static public int m_StaticMonsters;

    public static string LevelName
    {
        get { return m_LevelName; }
        set { m_LevelName = value; }
    }

    public static int ConsumableIn
    {
        get { return m_ConsumableIn; }
        set { m_ConsumableIn = value; }
    }

    public static int StaticMonsters
    {
        get { return m_StaticMonsters; }
        set { m_StaticMonsters = value; }
    }
}
