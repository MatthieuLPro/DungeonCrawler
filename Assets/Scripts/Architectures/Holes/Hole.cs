using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        /* Logic:
            Dans la partie player :
            - Il faut enregistrer le dernier le dernier mouvement du player
            - Il faut enregistrer la dernière direction du player
            - Il faut ajouter un boolean "solid_path = true" if false on ne sauvegarde pas le dernier mouvement et direction
            - Il faut teleporter le player dans sa dernière direction avec solid_path == true
         */

         other.GetComponent<Player>().LooseLife(1);
         other.GetComponent<Transform>().position = other.GetComponent<Transform>().position - new Vector3(0, 1.5f, 0);
    }
}
