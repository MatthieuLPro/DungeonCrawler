using UnityEngine;

public class RedRuby : Ruby
{
    private void Awake()
    {
        value = 10;
        GetComponent<Animator>().SetInteger("Color", 2);
    }
}
