using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerPlayer1 : MonoBehaviour
{

    public static bool IsAxisInUse() 
    {
        return (MainHorizontal() != 0 || MainVertical() != 0);
    }

    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("P1_Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("P1_Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainJoystick()
    {
        return new Vector3(MainHorizontal(), MainVertical());
    }

    public static bool AButton()
    {
        return Input.GetButtonDown("P1_Action");
    }

    // Action button
    public static bool BButton()
    {
        return Input.GetButtonDown("P1_Attack");
    }

    public static bool XButton()
    {
        return Input.GetButtonDown("P1_Consommable");
    }

    public static bool YButton()
    {
        return Input.GetButtonDown("P1_Equipement");
    }

    public static bool LButton()
    {
        return Input.GetButtonDown("L_Button");
    }

    public static bool RButton()
    {
        return Input.GetButtonDown("R_Button");
    }

    public static bool LTrigger()
    {
        return (Input.GetAxisRaw("Triggers") == -1);
    }

    public static bool RTrigger()
    {
        return (Input.GetAxisRaw("Triggers") == 1);
    }
}
