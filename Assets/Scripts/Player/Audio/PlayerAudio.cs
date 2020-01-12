using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource _audioData;

    void Start(){
        _audioData = GetComponent<AudioSource>();
    }

    public void CallAudio(string animation)
    {
        ChangeAudioClip(animation);
        _audioData.Play();
    }


    private void ChangeAudioClip(string animation)
    {
        AudioClip clip = null;

        switch(animation)
        {
            case "attack":
                clip = Resources.Load("Sounds/LTTP_Sword1") as AudioClip;
                break;
            case "hurt":
                clip = Resources.Load("Sounds/LTTP_Link_Hurt") as AudioClip;
                break;
            default:
                break;
        }
        _audioData.clip = clip;
    }
}
