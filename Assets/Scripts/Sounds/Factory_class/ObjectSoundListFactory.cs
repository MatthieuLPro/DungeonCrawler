using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSoundListFactory : MonoBehaviour
{
    public abstract IObjectSoundList Create();
}
