using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundList : IObjectSoundList
{
    public AudioClip UpdateAudioClip(string animation)
    {
        switch(animation)
        {
            case "hurt":
                return Resources.Load("Sounds/Enemies/enemy_hit") as AudioClip;
                break;
            case "ko":
                return Resources.Load("Sounds/Enemies/enemy_ko") as AudioClip;
                break;
            case "bullet":
                return Resources.Load("Sounds/Enemies/bullet") as AudioClip;
                break;
            default:
                return Resources.Load("Sounds/Enemies/enemy_ko") as AudioClip;
                break;
        }
    }
}
