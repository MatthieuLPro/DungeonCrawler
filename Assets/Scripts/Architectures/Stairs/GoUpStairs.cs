using UnityEngine;

public class GoUpStairs : Stairs
{
    protected override void StairsInteraction(GameObject player)
    {
        if(player.GetComponent<PlayerController>().maxSpeedTemp >= player.GetComponent<PlayerController>().maxSpeed)
            return;

        player.GetComponent<PlayerController>().maxSpeedTemp    = player.GetComponent<PlayerController>().maxSpeed;
        player.GetComponent<SpriteRenderer>().sortingLayerName  = transform.parent.GetComponent<StairsCharacteristics>().layoutUp;
        player.layer = transform.parent.GetComponent<StairsCharacteristics>().layerUp;
    }
}
