using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresDungeon : MonoBehaviour
{
    const string rank_1 = "1st";
    const string rank_2 = "2nd";
    const string rank_3 = "3rd";
    const string rank_4 = "4th";

    private ResultPlayer _resultPlayer1;
    private ResultPlayer _resultPlayer2;
    private ResultPlayer _resultPlayer3;
    private ResultPlayer _resultPlayer4;

    private int _scorePlayer1 = 0;
    private int _scorePlayer2 = 0;
    private int _scorePlayer3 = 0;
    private int _scorePlayer4 = 0;

    public Text timerText   = null;
    public Text player1Text = null;
    public Text player2Text = null;
    public Text player3Text = null;
    public Text player4Text = null;
    private float startTime;

    private string[] _rank = new string[4];

    void Start()
    {
        _resultPlayer1 = transform.parent.Find("Player_1").GetComponent<ResultPlayer>();
        _resultPlayer2 = transform.parent.Find("Player_2").GetComponent<ResultPlayer>();
        _resultPlayer3 = transform.parent.Find("Player_3").GetComponent<ResultPlayer>();
        _resultPlayer4 = transform.parent.Find("Player_4").GetComponent<ResultPlayer>();

        _rank[0] = "player_1";
        if (_resultPlayer2) _rank[1] = "player_2";
        if (_resultPlayer3) _rank[2] = "player_3";
        if (_resultPlayer4) _rank[3] = "player_4";

        startTime = Time.time;
        player1Text.text = rank_1;
        player2Text.text = rank_2;
        player3Text.text = rank_3;
        player4Text.text = rank_4;
    }

    void Update() {
        _UpdateTimer();
    }

    // Update call by resultPlayer
    public void UpdateScore(ResultPlayer resultPlayer){
        if (resultPlayer == _resultPlayer1)
            _scorePlayer1 = _resultPlayer1.Score;
        if (resultPlayer == _resultPlayer2)
            _scorePlayer2 = _resultPlayer2.Score;
        if (resultPlayer == _resultPlayer3)
            _scorePlayer3 = _resultPlayer3.Score;
        if (resultPlayer == _resultPlayer4)
            _scorePlayer4 = _resultPlayer4.Score;

        _UpdateRank();
    }

    void _UpdateTimer() {
        float t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    void _UpdateRank() {
    }
}
