﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundList : MonoBehaviour
{
    private Dictionary<string, ObjectSoundListFactory> _factory;

    public ObjectSoundList()
    {
        _factory = new Dictionary<string, ObjectSoundListFactory>
        {
            { "Player", new PlayerSoundListFactory() },
            { "Enemy",  new MonsterSoundListFactory() }
        };
    }

    public IObjectSoundList ExecuteCreation(string typeObject) => _factory[typeObject].Create();
}