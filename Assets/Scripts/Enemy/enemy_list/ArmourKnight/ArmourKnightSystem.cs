using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourKnightSystem : MonoBehaviour
{

    [Header("Armour knights List")]
    [SerializeField]
    private GameObject[] _armourKnights;

    [Header("Phase Frequence Settings")]
    [SerializeField]
    private float _phaseTime        = .0f;
    [SerializeField]
    private float _transitionTime   = .0f;

    private bool _coIsWorking;

    public int phase          = 0;

    void Start(){
        _coIsWorking    = false;
    }    

    void Update()
    {
        if (phase == 1)
        {
            if(KnightsTransitionReady() && !_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(phase + 1, _phaseTime));
        }
        else if (phase == 2)
        {
            if (!_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(phase + 1, _phaseTime));
        }
        else if (phase == 3)
        {
            if(KnightsTransitionReady() && !_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(phase + 1, _phaseTime));
        }
        else if (phase == 4)
        {
            if (!_coIsWorking)
                StartCoroutine(TransitionAndPhaseCo(1, _phaseTime));
        }
    }

    private bool KnightsTransitionReady()
    {
        for(var i = 0; i < _armourKnights.Length; i++)
        {
            if (!_armourKnights[i].transform.GetChild(0).GetComponent<ArmourKnightPattern>().ready)
                return false;
        }

        return true;
    }

    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */
    // Transition and Phase wait CoRoutine
    private IEnumerator TransitionAndPhaseCo(int nextPhase, float waitTime)
    {
        _coIsWorking = true;
        yield return new WaitForSeconds(waitTime);
        
        for(var i = 0; i < _armourKnights.Length; i++)
            _armourKnights[i].transform.GetChild(0).GetComponent<ArmourKnightPattern>().ready = false;

        phase          = nextPhase;
        _coIsWorking    = false;
    }
}
