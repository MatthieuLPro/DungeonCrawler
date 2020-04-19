using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCutSceneManager : MonoBehaviour
{
    public float _timeLineLength = 0f;
    public GameParameters _gameParams = null;
    void Awake(){
        _gameParams = transform.root.Find("GameParameters").GetComponent<GameParameters>();
        // Need to get timeLength from => PlayableDirector
        _timeLineLength = 10f;
        StartCoroutine(TimelineCo(_timeLineLength));
    }

    // Need to add nb of validation verification before skip cutscene
    public void OnAButton(){
        _launchRace();
    }

    public IEnumerator TimelineCo(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        _launchRace();
    }

    private void _launchRace() {
        _gameParams.SetDungeon();
        Destroy(transform.parent.gameObject);
    }
}
