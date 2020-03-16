using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour
{
    private ScoresDungeon _scoreDungeon;

    [Header("Result parameters")]
    public int rubyInit = 0;

    void Start() {
        _scoreDungeon = transform.parent.Find("Scores").GetComponent<ScoresDungeon>();
    }

    public void GetRuby(int rubyAmount){
        transform.Find("UI").Find("Rubies").Find("RubyTextUI").GetComponent<RubyUI>().rubySystem.ChangeRuby(rubyAmount);
        _scoreDungeon.UpdateScore(this, rubyAmount);
    }

    public void FinishDungeon(int finishDungeonValue){
        _scoreDungeon.UpdateScore(this, finishDungeonValue);
    }

    public void EnterFirstInRoom(int roomValue){
        _scoreDungeon.UpdateScore(this, roomValue);
    }

    public void KillMonster(){
    }

    public void KillBoss(){
    }

    public void AmountDamageBoss(){
    }
}