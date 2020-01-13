using UnityEngine;

public class TakeStairs : Stairs
{
    protected override void InteractionWithStairs(GameObject player)
    {
        player.GetComponent<Movement>().maxSpeedTemp = player.GetComponent<Movement>().maxSpeed / 4;
        player.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName = transform.parent.GetComponent<StairsCharacteristics>().GetSortLayerUp();
    }

    protected override void SetLayersValue(){}
}
