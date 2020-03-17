using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoresDungeon : MonoBehaviour
{
    private ResultPlayer _resultPlayer1;
    private ResultPlayer _resultPlayer2;
    private ResultPlayer _resultPlayer3;
    private ResultPlayer _resultPlayer4;

    public Text timerText   = null;
    public Text player1Text = null;
    public Text player2Text = null;
    public Text player3Text = null;
    public Text player4Text = null;
    private float startTime;

    public string[] _rank = new string[4];
    public string[] _rankPlayer1 = new string[2];
    public string[] _rankPlayer2 = new string[2];
    public string[] _rankPlayer3 = new string[2];
    public string[] _rankPlayer4 = new string[2];

    public string[] _rankText       = new string[4];
    public string[][] _rankGlobal   = new string[4][];

    public int nb_players;

    void Start()
    {
        _resultPlayer1 = transform.parent.Find("Player_1").GetComponent<ResultPlayer>();
        _resultPlayer2 = transform.parent.Find("Player_2").GetComponent<ResultPlayer>();
        _resultPlayer3 = transform.parent.Find("Player_3").GetComponent<ResultPlayer>();
        _resultPlayer4 = transform.parent.Find("Player_4").GetComponent<ResultPlayer>();

        _rankText = new string[4] {"4th", "3rd", "2nd", "1st"};

        _rank[0]            = "player_1";
        _rankPlayer1        = new string[2] {_rankText[3], "0"};
        player1Text.text    = _rankPlayer1[0];
        _rankGlobal[0]      = _rankPlayer1;

        nb_players += 1;

        if (_resultPlayer2) {
            _rank[1]            = "player_2";
            _rankPlayer2        = new string[2] {_rankText[3], "0"};
            player2Text.text    = _rankPlayer2[0];
            _rankGlobal[1]      = _rankPlayer2;
            nb_players += 1;
        }
        if (_resultPlayer3) {
            _rank[2]            = "player_3";
            _rankPlayer3        = new string[2] {_rankText[3], "0"};
            player3Text.text    = _rankPlayer3[0];
            _rankGlobal[2]      = _rankPlayer3;
            nb_players += 1;
        }
        if (_resultPlayer4) {
            _rank[3]            = "player_4";
            _rankPlayer4        = new string[2] {_rankText[3], "0"};
            player4Text.text    = _rankPlayer4[0];
            _rankGlobal[3]      = _rankPlayer4;
            nb_players += 1;
        }
        startTime = Time.time;
    }

    void Update() {
        _UpdateTimer();
    }

    // Update call by resultPlayer
    public void UpdateScore(ResultPlayer resultPlayer, int points){
        if (resultPlayer == _resultPlayer1)
            _rankPlayer1[1] = (int.Parse(_rankPlayer1[1]) + points).ToString();
        if (resultPlayer == _resultPlayer2)
            _rankPlayer2[1] = (int.Parse(_rankPlayer2[1]) + points).ToString();
        if (resultPlayer == _resultPlayer3)
            _rankPlayer3[1] = (int.Parse(_rankPlayer3[1]) + points).ToString();
        if (resultPlayer == _resultPlayer4)
            _rankPlayer4[1] = (int.Parse(_rankPlayer4[1]) + points).ToString();

        _UpdateRankScore();
        _UpdateRankText();
    }

    // Increment timer and update text
    void _UpdateTimer() {
        float t = Time.time - startTime;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }

    // Update player rank
    void _UpdateRankScore() {
        int rank = 0;
        for(int i = 0; i < nb_players; i++) {
            for(int j = 0; j < nb_players; j++) {
                if (int.Parse(_rankGlobal[i][1]) > int.Parse(_rankGlobal[j][1])) {
                    rank += 1;
                }
            }
            _rankGlobal[i][0] = _rankText[rank];
            rank = 0;
        }
    }

    // Update player rank text
    void _UpdateRankText() {
        player1Text.text = _rankPlayer1[0];
        if (_resultPlayer2) player2Text.text = _rankPlayer2[0];
        if (_resultPlayer3) player3Text.text = _rankPlayer3[0];
        if (_resultPlayer4) player4Text.text = _rankPlayer4[0];
    }

    // Debug function (need to clean when score dungeon is finish)
    void showValue() {
        for(int i = 0; i < 4; i++) {
            Debug.Log("Value for " + i + " " + _rankGlobal[i][0] + " : " + _rankGlobal[i][1]);
        }
    }
}
