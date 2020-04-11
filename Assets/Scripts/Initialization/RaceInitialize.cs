using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class RaceInitialize : MonoBehaviour
{
    public GameObject characterPrefab;

    private PlayerInputManager _playerInputManager;
    private GameParameters _gameParameters;

    void Start()
    {
        _gameParameters = gameObject.GetComponent<GameParameters>();
        _playerInputManager = gameObject.GetComponent<PlayerInputManager>();
        _InitializePlayers();
    }

    private void _InitializePlayers() 
    {
        int playersNumber = _gameParameters.PlayersNumber;
        for (int playerIndex = 1; playerIndex <= playersNumber; playerIndex++) 
        {
            //GameObject playerGameObject = Instantiate(_playerPrefab) as GameObject;
            //playerGameObject.GetComponent<Player>().PlayerIndex = playerIndex;
        }
            
        //int playersNumber = _gameParameters.GetPlayersNumber();
    }

    private void OnPlayerJoined(PlayerInput input){
        Debug.Log("On Player Joindes");
    }
}
