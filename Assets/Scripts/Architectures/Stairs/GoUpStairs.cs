using UnityEngine;

public class GoUpStairs : Stairs
{
    protected override void StairsInteraction(GameObject player)
    {
        if(player.GetComponent<Movement>().maxSpeedTemp >= player.GetComponent<Movement>().maxSpeed)
            return;

        player.GetComponent<Movement>().maxSpeedTemp = player.GetComponent<Movement>().maxSpeed;
        player.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName  = transform.parent.GetComponent<StairsCharacteristics>().layoutUp;
        player.layer = transform.parent.GetComponent<StairsCharacteristics>().layerUp;
    }
}
