using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayer : MonoBehaviour
{
    [Header("Result parameters")]
    public int rubyInit = 0;

    public void GetRuby(int rubyAmount){
        transform.parent.Find("UI").Find("RubyTextUI").GetComponent<RubyUI>().rubySystem.ChangeRuby(rubyAmount);
    }
}
