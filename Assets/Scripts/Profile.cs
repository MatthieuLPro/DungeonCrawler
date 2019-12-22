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
    public string nickName;
    public enum Character {
        character_1,
        character_2,
        character_3
    };

    void Start()
    {
        //Load value from a file
    }

    void createProfile()
    {
        //Create a profile and save it in a file
    }

    void createTempProfile()
    {
        //Create a temporary profile with temporary data
    }

    void updateProfile()
    {
        //Update a profile and save it in a file
    }

    void updateTempProfile()
    {
        //Update a temporary profile with temporary data
    }
}
