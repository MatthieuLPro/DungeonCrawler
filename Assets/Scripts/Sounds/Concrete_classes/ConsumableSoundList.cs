using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSoundList : IObjectSoundList
{
    public AudioClip UpdateAudioClip(string animation)
    {
        AudioClip newAudioClip = null;
        switch(animation)
        {
            case "search":
                newAudioClip = Resources.Load("Sounds/Objects/Consumables/ConsumableBoxSearch") as AudioClip;
                break;
            case "find":
                newAudioClip = Resources.Load("Sounds/Objects/Consumables/ConsumableBoxFind") as AudioClip;
                break;
            default:
                newAudioClip = Resources.Load("Sounds/Objects/Consumables/ConsumableBoxSearch") as AudioClip;
                break;
        }
        return newAudioClip;
    }
}
