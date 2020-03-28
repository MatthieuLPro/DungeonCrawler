using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObjectSoundList : IObjectSoundList
{
    public AudioClip UpdateAudioClip(string animation)
    {
        AudioClip newAudioClip = null;
        switch(animation)
        {
            case "openSmall":
                newAudioClip = Resources.Load("Sounds/Objects/Treasures/OpenSmallTreasure") as AudioClip;
                break;
            case "openBig":
                newAudioClip = Resources.Load("Sounds/Objects/Treasures/OpenBigTreasure") as AudioClip;
                break;
            default:
                newAudioClip = Resources.Load("Sounds/Objects/Treasures/CantOpen") as AudioClip;
                break;
        }
        return newAudioClip;
    }
}
