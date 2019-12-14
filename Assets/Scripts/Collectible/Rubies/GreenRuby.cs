using UnityEngine;

public class GreenRuby : Ruby
{
    private void Awake()
    {
        value = 1;
        GetComponent<Animator>().SetInteger("Color", 0);
    }
}
