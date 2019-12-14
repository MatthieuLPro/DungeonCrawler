using UnityEngine;

public class BlueRuby : Rubies
{
    private void Awake()
    {
        value = 5;
        GetComponent<Animator>().SetInteger("Color", 1);
    }
}
