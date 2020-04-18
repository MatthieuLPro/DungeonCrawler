using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public string _name = "";
    public int _nbStep = 0;
    public int _nbAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string Name {
        get => _name;
        set => _name = value;
    }

    public int NbStep {
        get => _nbStep;
        set => _nbStep += 1;
    }

    public int NbAttack {
        get => _nbAttack;
        set => _nbAttack += 1;
    }
}
