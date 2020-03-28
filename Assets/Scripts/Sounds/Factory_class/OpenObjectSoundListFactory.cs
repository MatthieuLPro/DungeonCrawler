using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObjectSoundListFactory :  ObjectSoundListFactory {    
    public override IObjectSoundList Create() => new OpenObjectSoundList();
}
