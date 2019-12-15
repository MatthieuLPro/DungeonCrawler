using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonVerification : MonoBehaviour
{
    /* ************************************************ */
    /* Color Switch condition */
    /* ************************************************ */
    /* Color switch */
    public bool ColorSwitch(GameObject colorSwitch){
        return colorSwitch.GetComponent<SwitchColor>().color;
    }
}
