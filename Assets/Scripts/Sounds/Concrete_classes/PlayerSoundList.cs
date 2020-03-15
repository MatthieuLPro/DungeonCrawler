using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundList : IObjectSoundList
{
    public AudioClip UpdateAudioClip(string animation)
    {
        AudioClip newAudioClip = null;

        switch(animation)
        {
            case "attack":
                newAudioClip = Resources.Load("Sounds/Players/player_attack") as AudioClip;
                break;
            case "hurt":
                newAudioClip = Resources.Load("Sounds/Players/player_hurt") as AudioClip;
                break;
            default:
                newAudioClip = Resources.Load("Sounds/Players/player_hurt") as AudioClip;
                break;
        }
        return newAudioClip;
    }
}
