using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

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
        int playersNumber = _gameParameters.GetPlayersNumber();
        _playerInputManager.fixedNumberOfSplitScreens = playersNumber;
    }
}
