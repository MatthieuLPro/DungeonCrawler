using UnityEngine;

public class GoDownStairs : Stairs
{
    protected override void StairsInteraction(GameObject player)
    {
        if(player.GetComponent<PlayerController>().maxSpeedTemp >= player.GetComponent<PlayerController>().maxSpeed)
            return;

        player.GetComponent<PlayerController>().maxSpeedTemp    = player.GetComponent<PlayerController>().maxSpeed;
        player.GetComponent<SpriteRenderer>().sortingLayerName  = transform.parent.GetComponent<StairsCharacteristics>().layoutDown;
        player.layer = transform.parent.GetComponent<StairsCharacteristics>().layerDown;
    }

}
