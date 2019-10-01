using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : MonoBehaviour
{
    private BoxCollider2D box;

    private void Start(){
        box = GetComponent<BoxCollider2D>();
    }

    public void ChangeSprite(bool down)
    {
        if (down)
            box.enabled = false;
        else
            box.enabled = true;
    }
}
