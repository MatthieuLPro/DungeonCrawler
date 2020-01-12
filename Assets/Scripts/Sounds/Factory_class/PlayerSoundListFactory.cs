using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundListFactory : ObjectSoundListFactory
{
    public override IObjectSoundList Create() => new PlayerSoundList();
}
