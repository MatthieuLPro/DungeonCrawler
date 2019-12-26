using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour
{
    [Header("Result parameters")]
    public int rubyInit = 0;

    public void GetRuby(int rubyAmount){
        RubyUI.rubySystemStatic.ChangeRuby(rubyAmount);
    }
}
