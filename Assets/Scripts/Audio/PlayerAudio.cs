using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioSource audioData;

    void Start(){
        audioData = GetComponent<AudioSource>();
    }

    public void CallAudio(string animation)
    {
        ChangeAudioClip(animation);
        audioData.Play();
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
        audioData.clip = clip;
    }
}
