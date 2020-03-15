using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundList : IObjectSoundList
{
    public AudioClip UpdateAudioClip(string animation)
    {
        AudioClip newAudioClip = null;
        switch(animation)
        {
            case "hurt":
                newAudioClip = Resources.Load("Sounds/Enemies/enemy_hit") as AudioClip;
                break;
            case "ko":
                newAudioClip = Resources.Load("Sounds/Enemies/enemy_ko") as AudioClip;
                break;
            case "bullet":
                newAudioClip = Resources.Load("Sounds/Enemies/bullet") as AudioClip;
                break;
            default:
                newAudioClip = Resources.Load("Sounds/Enemies/enemy_ko") as AudioClip;
                break;
        }
        return newAudioClip;
    }
}
