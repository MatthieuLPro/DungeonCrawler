using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundListFactory : ObjectSoundListFactory
{
    public override IObjectSoundList Create() => new MonsterSoundList();
}
