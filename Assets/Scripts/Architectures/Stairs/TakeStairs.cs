using UnityEngine;

public class TakeStairs : Stairs
{
    protected override void StairsInteraction(GameObject player)
    {
        player.GetComponent<PlayerController>().maxSpeedTemp = player.GetComponent<PlayerController>().maxSpeed / 2;
        player.GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.GetComponent<StairsCharacteristics>().layoutUp;
    }
}
