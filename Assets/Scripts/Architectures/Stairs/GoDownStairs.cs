using UnityEngine;

public class GoDownStairs : Stairs
{
    protected override void StairsInteraction(GameObject player)
    {
        if(player.GetComponent<Movement>().maxSpeedTemp >= player.GetComponent<Movement>().maxSpeed)
            return;

        player.GetComponent<Movement>().maxSpeedTemp = player.GetComponent<Movement>().maxSpeed;
        player.transform.parent.GetComponent<SpriteRenderer>().sortingLayerName  = transform.parent.GetComponent<StairsCharacteristics>().GetSortLayerDown();
        player.transform.parent.gameObject.layer = transform.parent.GetComponent<StairsCharacteristics>().GetLayerDown();
    }

}
