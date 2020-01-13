using UnityEngine;

public class GoUpStairs : Stairs
{
    protected override void InteractionWithStairs(GameObject player){
        EnterInStairs(player);
    }

    protected override void SetLayersValue()
    {
        layer = transform.parent.GetComponent<StairsCharacteristics>().GetLayerUp();
        sortLayer = transform.parent.GetComponent<StairsCharacteristics>().GetSortLayerUp();
    }
}
