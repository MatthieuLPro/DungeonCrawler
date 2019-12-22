using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    /* Existe 2 types de data:
        - Data que l'on conserve (sauvegarde dans un fichier)
            - nickName
            - statistics (nombre de km parcouru etc ...)
        - Data que l'on ne conserver pas
            - personnage joué
            - skin utilisé
    */ 
    public enum Character {
        character_1,
        character_2,
        character_3
    };

    public string       nickName;
    public Character    characterChoosen;

    //Init empty profile object when enter in game 
    void Start()
    {
        nickName         = "";
        characterChoosen = Character.character_1;
    }

    //Create a profile from user input with normal values and save in JSON
    void createProfile(string newNickName)
    {
        nickName = newNickName;
        //Save in JSON files
    }

    //Update normal values and save in JSON
    void updateProfile(string newNickName)
    {
        nickName = newNickName;
        //Save in JSON files
    }

    //Update temp values from user input
    void updateTempProfile(Character playerValue)
    {
        characterChoosen = playerValue;
    }

    // Load all normal values from JSON
    void loadProfile(string nickNameLoad)
    {
        nickName = nickNameLoad;
    }
}
