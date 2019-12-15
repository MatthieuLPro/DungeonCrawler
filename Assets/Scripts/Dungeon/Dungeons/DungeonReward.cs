using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonReward : MonoBehaviour
{
    /* ************************************************ */
    /* Color Switches Consequences */
    /* ************************************************ */
    /* Red Switch */
    public void SwitchRedColor(GameObject switchColor)
    {
        switchColor.GetComponent<SwitchColor>().color = true;
        switchColor.GetComponent<SpriteRenderer>().sprite = switchColor.GetComponent<SwitchColor>().redSprite;
    }

    /* Blue Switch */
    public void SwitchBlueColor(GameObject switchColor)
    {
        switchColor.GetComponent<SwitchColor>().color = false;
        switchColor.GetComponent<SpriteRenderer>().sprite = switchColor.GetComponent<SwitchColor>().blueSprite;
    }

    /* Up bloc */
    public void BlockUp(GameObject blockColor)
    {
        blockColor.GetComponent<SpriteRenderer>().sprite = blockColor.GetComponent<BlockColor>().upSprite;
        blockColor.GetComponent<BoxCollider2D>().enabled = true;
    }

    /* Down bloc */
    public void BlockDown(GameObject blockColor)
    {
        blockColor.GetComponent<SpriteRenderer>().sprite = blockColor.GetComponent<BlockColor>().downSprite;
        blockColor.GetComponent<BoxCollider2D>().enabled = false;
    }
}
