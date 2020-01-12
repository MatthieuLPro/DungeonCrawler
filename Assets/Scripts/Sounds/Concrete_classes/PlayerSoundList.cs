using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundList : IObjectSoundList
{
    public AudioClip UpdateAudioClip(string animation)
    {
        switch(animation)
        {
            case "attack":
                return Resources.Load("Sounds/Players/player_attack") as AudioClip;
                break;
            case "hurt":
                return Resources.Load("Sounds/Players/player_hurt") as AudioClip;
                break;
            default:
                return Resources.Load("Sounds/Players/player_hurt") as AudioClip;
                break;
        }
    }
}
