using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum emoteType
{
    nothing = 0,
    talk = 1,
    nervous = 2,
    happy = 3,
    confused = 4,
    alerted = 5
};

public class emotesManager : MonoBehaviour
{
    private Animator _animator;
    private int _currentEmoteType;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();    
    }

    void setEmoteType(int emote)
    {
        _currentEmoteType = emote;
        _animator.SetInteger("emoteType", _currentEmoteType);
    }
}
