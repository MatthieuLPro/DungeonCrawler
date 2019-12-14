using UnityEngine;

public class BlueRuby : Ruby
{
    private void Awake()
    {
        value = 5;
        GetComponent<Animator>().SetInteger("Color", 1);
    }
}
