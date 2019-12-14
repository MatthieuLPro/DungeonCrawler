using UnityEngine;

public class GreenRuby : Rubies
{
    private void Awake()
    {
        value = 1;
        GetComponent<Animator>().SetInteger("Color", 0);
    }
}
