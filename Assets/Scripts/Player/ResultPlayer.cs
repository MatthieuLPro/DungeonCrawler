using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour
{
    private int _score;
    private ScoresDungeon _scoreDungeon;

    [Header("Result parameters")]
    public int rubyInit = 0;

    void Start() {
        _score = 0;
        _scoreDungeon = transform.Find("Scores").GetComponent<ScoresDungeon>();
    }

    public void GetRuby(int rubyAmount){
        transform.parent.Find("UI").Find("Rubies").Find("RubyTextUI").GetComponent<RubyUI>().rubySystem.ChangeRuby(rubyAmount);
        Score = rubyAmount;
        _scoreDungeon.UpdateScore(this);
    }

    public void FinishDungeon(){
    }

    public void EnterFirstInRoom(){
    }

    public void KillMonster(){
    }

    public void KillBoss(){
    }

    public void AmountDamageBoss(){
    }

    public int Score { 
        get { return _score; }
        set { _score += value; }
    }
}
