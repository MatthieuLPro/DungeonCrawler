using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMode
{
    singlePlayer = 1,
    twoPlayers = 2,
    threePlayers = 3,
    fourPlayers = 4
};

public enum DifficultyMode
{
    easy,
    medium,
    hard,
    speedrunner
};

public class level_manager : MonoBehaviour
{
    //public PlayerMode SelectedPlayerMode;
    public DifficultyMode SelectedDifficultyMode;

    public GameObject[] PlayersList = new GameObject[3];
    public GameObject PlayerPrefab;

    public RaceParameters raceParameters;

    private int _playersCount;

    // Start is called before the first frame update
    void Start()
    {
        _initializePlayers(raceParameters.GetPlayersCount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void _InitializeLevel() 
    {
        /*for (int playerIndex = 1; playerIndex <= playersCount; playerIndex++)
        {
            _initializePlayer(playerIndex);
        }*/
    }
    private void _initializePlayers(int playersCount)
    {
        _playersCount = playersCount;
        for (int playerIndex = 0; playerIndex <= _playersCount; playerIndex++) 
        {
            GameObject playerGameObject = Instantiate(PlayerPrefab) as GameObject;
            GameObject cameraGameObject = playerGameObject.transform.Find("Camera").gameObject;
            Camera playerCamera = cameraGameObject.GetComponent<Camera>();
            playerCamera.rect = _getCameraParametersFromPlayerIndex(playerIndex);
        }
    }

    private Rect _getCameraParametersFromPlayerIndex(int playerIndex) 
    {
        Rect cameraRect = new Rect();
        switch (_playersCount) 
        {
            case (int) PlayerMode.singlePlayer:
            default:
                cameraRect.x = 0;
                cameraRect.y = 0;
                cameraRect.width = 1;
                cameraRect.height = 1f;
                break;
            case (int) PlayerMode.twoPlayers:
                cameraRect.x = 0;
                cameraRect.width = 1;
                cameraRect.height = 0.5f;
                if (playerIndex == 0)
                    cameraRect.y = 0;
                else if (playerIndex == 1) 
                    cameraRect.y = 0.5f;
                break;
            case (int) PlayerMode.threePlayers:
                cameraRect.width = 0.5f;
                cameraRect.height = 0.5f;
                if (playerIndex == 0) 
                {
                    cameraRect.x = 0;
                    cameraRect.y = 0.5f;
                }
                else if (playerIndex == 1)
                {
                    cameraRect.x = 0.5f;
                    cameraRect.y = 0.5f;
                }
                else if (playerIndex == 2)
                {
                    cameraRect.x = 0;
                    cameraRect.y = 0f;
                } 
                break;
            case (int) PlayerMode.fourPlayers:
                cameraRect.width = 0.5f;
                cameraRect.height = 0.5f;
                if (playerIndex == 0)
                {
                    cameraRect.x = 0;
                    cameraRect.y = 0;
                }
                else if (playerIndex == 1)
                {
                    cameraRect.x = 0.5f;
                    cameraRect.y = 0;
                }
                else if (playerIndex == 2)
                {
                    cameraRect.x = 0;
                    cameraRect.y = 0.5f;
                }
                else if (playerIndex == 3)
                {
                    cameraRect.x = 0.5f;
                    cameraRect.y = 0.5f;
                }
                break;
        }

        return cameraRect;
    }
}
