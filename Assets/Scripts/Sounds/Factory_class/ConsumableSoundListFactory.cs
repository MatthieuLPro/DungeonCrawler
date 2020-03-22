using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSoundListFactory : ObjectSoundListFactory {    
    public override IObjectSoundList Create() => new ConsumableSoundList();
}
