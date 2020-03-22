using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundList : MonoBehaviour
{
    private Dictionary<string, ObjectSoundListFactory> _factory;

    public ObjectSoundList()
    {
        _factory = new Dictionary<string, ObjectSoundListFactory>
        {
            { "PlayerAudio", new PlayerSoundListFactory() },
            { "EnemyAudio",  new MonsterSoundListFactory() },
            { "ConsumableAudio",  new ConsumableSoundListFactory() },
        };
    }

    public IObjectSoundList ExecuteCreation(string typeObject) => _factory[typeObject].Create();
}
