using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInitialize : MonoBehaviour
{
    private GameParameters _gameParameters;

    [SerializeField]
    public GameObject _playerPrefab;

    void Start()
    {
        _gameParameters = gameObject.GetComponent<GameParameters>();
        _InitializePlayers();
    }

    private void _InitializePlayers() 
    {
        int playersNumber = _gameParameters.PlayersNumber;
        for (int playerIndex = 1; playerIndex <= playersNumber; playerIndex++) 
        {
            GameObject playerGameObject = Instantiate(_playerPrefab) as GameObject;
            playerGameObject.GetComponent<Player>().SetPlayerIndex(playerIndex);
        }
            
    }
}
