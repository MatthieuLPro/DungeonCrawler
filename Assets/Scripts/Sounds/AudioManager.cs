using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource      _audioData;
    private IObjectSoundList _factory;

    private string _actualSound;

    void Start()
    {        
        _factory = new ObjectSoundList().ExecuteCreation(gameObject.tag);
        _audioData = GetComponent<AudioSource>();
    }

    /* Verify, update and play audio clip */
    public void CallAudio(string animation)
    {
        if (_actualSound != animation)
        {
            SetAudioClip(animation);
            SaveActualSound(animation);
        }

        PlayAudioClip();
    }

    /* *************** */
    /* Private methods */
    /* *************** */

    /* Play audio clip */
    private void PlayAudioClip(){
        _audioData.Play();
    }

    /* Update audio clip from factory */
    private void SetAudioClip(string animation){
        _audioData.clip = _factory.UpdateAudioClip(animation);
    }

    /* Save acutal sound to not recall update method */
    private void SaveActualSound(string animation){
        _actualSound = animation;
    }
}
