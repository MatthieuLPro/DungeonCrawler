using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExitButton : UIInput
{
    public override void InputValidation()
    {
        Application.Quit();
    }
}
