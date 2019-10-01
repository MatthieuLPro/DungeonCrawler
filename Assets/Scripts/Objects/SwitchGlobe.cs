using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGlobe : MonoBehaviour
{
    [Header("Switch Globe Parameters")]
    [SerializeField]
    private bool _color = false;
    [SerializeField]
    private Sprite _redSprite = null;
    [SerializeField]
    private Sprite _blueSprite = null;

    // Color : false = red / true = blue

    public void ToggleSwitchBlocks()
    {
        if (_color)
        {
            GetComponent<SpriteRenderer>().sprite = _blueSprite;
            SwitchBlocks("BlocBlue", false);
            SwitchBlocks("BlocRed", true);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = _redSprite;
            SwitchBlocks("BlocBlue", true);
            SwitchBlocks("BlocRed", false);
        }
        _color = !_color;
    }

    private void SwitchBlocks(string tagName, bool down)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);

        for(var i = 0; i < objects.Length; i++)
            objects[i].GetComponent<SwitchBlock>().ChangeSprite(down);
    }
}
