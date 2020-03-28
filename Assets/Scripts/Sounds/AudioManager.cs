using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource      _audioData;
    private IObjectSoundList _factory;

    private string _actualSound;

    void Start(){        
        _factory = new ObjectSoundList().ExecuteCreation(gameObject.tag);
        _audioData = GetComponent<AudioSource>();
    }

    /* Play audio clip */
    public void PlayAudio(){
        _audioData.Play();
    }

    /* Verify and update audio clip */
    public void CallAudio(string animation){
        if (_actualSound == animation)
            return;

        _SetAudioClip(animation);
        _SaveActualSound(animation);
    }

    /* *************** */
    /* Private methods */
    /* *************** */
    /* Update audio clip from factory */
    private void _SetAudioClip(string animation){
        _audioData.clip = _factory.UpdateAudioClip(animation);
    }

    /* Save acutal sound to not recall update method */
    private void _SaveActualSound(string animation){
        _actualSound = animation;
    }
}
