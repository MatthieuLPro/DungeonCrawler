using UnityEngine;

public class GoUpStairs : Stairs
{
    protected override void StairsInteraction(GameObject player)
    {
        if(player.GetComponent<Movement>().maxSpeedTemp >= player.GetComponent<Movement>().maxSpeed)
            return;

        player.GetComponent<Movement>().maxSpeedTemp = player.GetComponent<Movement>().maxSpeed;
        player.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName  = transform.parent.GetComponent<StairsCharacteristics>().GetSortLayerUp();
        player.transform.parent.gameObject.layer = transform.parent.GetComponent<StairsCharacteristics>().GetLayerUp();
    }
}
