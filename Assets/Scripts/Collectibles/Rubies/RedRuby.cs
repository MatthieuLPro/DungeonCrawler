using UnityEngine;

public class RedRuby : Rubies
{
    private void Awake()
    {
        value = 10;
        GetComponent<Animator>().SetInteger("Color", 2);
    }
}
