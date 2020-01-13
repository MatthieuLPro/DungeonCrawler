using UnityEngine;

public class GoDownStairs : Stairs
{
    protected override void InteractionWithStairs(GameObject player){
        EnterInStairs(player);
    }

    protected override void SetLayersValue()
    {
        layer = transform.parent.GetComponent<StairsCharacteristics>().GetLayerDown();
        sortLayer = transform.parent.GetComponent<StairsCharacteristics>().GetSortLayerDown();
    }
}
