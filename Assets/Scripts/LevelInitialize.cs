using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitialize : MonoBehaviour
{
    public GameObject[] PlayersList = new GameObject[3];
    public GameObject PlayerPrefab;

    public RaceParameters raceParameters;

    private int _playersNumber;

    void Start()
    {
        _initializePlayers(raceParameters.GetPlayersNumber());
    }

    private void _initializePlayers(int playersNumber)
    {
        _playersNumber = playersNumber;
        for (int playerIndex = 0; playerIndex <= _playersNumber; playerIndex++)
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
        switch (_playersNumber)
        {
            case 1:
            default:
                cameraRect.x = 0;
                cameraRect.y = 0;
                cameraRect.width = 1;
                cameraRect.height = 1f;
                break;
            case 2:
                cameraRect.x = 0;
                cameraRect.width = 1;
                cameraRect.height = 0.5f;
                if (playerIndex == 0)
                    cameraRect.y = 0;
                else if (playerIndex == 1)
                    cameraRect.y = 0.5f;
                break;
            case 3:
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
            case 4:
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

