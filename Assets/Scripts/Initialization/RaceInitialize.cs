using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class RaceInitialize : MonoBehaviour
{
    [SerializeField] internal GameObject m_PlayerPrefabInstantiateZone;
    [SerializeField] internal List<GameObject> m_PlayerGameObjectList = new List<GameObject>();

    [NonSerialized] private PlayerInputManager _playerInputManager;
    [NonSerialized] private GameParameters _gameParameters;

    void Start()
    {
        _gameParameters = gameObject.GetComponent<GameParameters>();
        _playerInputManager = gameObject.GetComponent<PlayerInputManager>();
        _InitializePlayers();
    }

    private void _InitializePlayers() 
    {
        // USE THIS METHOD FOR V2
        //int playersNumber = _gameParameters.PlayersNumber;
        //for (int playerIndex = 1; playerIndex <= playersNumber; playerIndex++) 
        //{
            //GameObject playerGameObject = Instantiate(_playerPrefab) as GameObject;
            //playerGameObject.GetComponent<Player>().PlayerIndex = playerIndex;
        //}
            
        //int playersNumber = _gameParameters.PlayersNumber;
    }

    /*private void OnPlayerJoined(PlayerInput input){
        Debug.Log("On Player Joindes");
    }*/
}
