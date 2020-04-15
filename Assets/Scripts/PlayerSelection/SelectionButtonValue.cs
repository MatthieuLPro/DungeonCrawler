using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButtonValue : MonoBehaviour
{
    [SerializeField] internal GameObject m_StoredData;

    public GameObject Value
    {
        get => m_StoredData;
        set => m_StoredData = value;
    }
}
