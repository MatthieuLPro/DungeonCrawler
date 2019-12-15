using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMachine : MonoBehaviour
{
    // Add the debug machine to the gameObject Player
    // Buttons : page up / down => Increase / Decrease Game speed

    void Update()
    {
        if (Input.GetButtonDown("Slow Motion"))
            DecreaseGameSpeed();
        else if (Input.GetButtonDown("Speed Motion"))
            IncreaseGameSpeed();
    }

    private void DecreaseGameSpeed(){
        Time.timeScale -= 0.1f;
    }

    private void IncreaseGameSpeed(){
        Time.timeScale += 0.1f;
    }
}
