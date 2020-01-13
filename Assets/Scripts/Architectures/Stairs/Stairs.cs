using UnityEngine;

abstract public class Stairs : MonoBehaviour
{
    public int layer;       
    public string sortLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        InteractionWithStairs(other.transform.GetChild(0).gameObject);
    }

    public void _UpdateLayers(GameObject controller)
    {
        for(int i = 0; i < controller.transform.childCount; i++)
        {
            GameObject child = controller.transform.GetChild(i).gameObject;
            child.layer = layer;
            for(int j = 0; j < child.transform.childCount; j++)
                child.transform.GetChild(j).gameObject.layer = layer;
        }
    }

    /* Abstraction */
    protected abstract void InteractionWithStairs(GameObject player);
    protected abstract void SetLayersValue();

    public void EnterInStairs(GameObject player)
    {
        if(player.GetComponent<Movement>().maxSpeedTemp >= player.GetComponent<Movement>().maxSpeed)
            return;

        player.GetComponent<Movement>().maxSpeedTemp = player.GetComponent<Movement>().maxSpeed;
        
        GameObject controller   = player.transform.parent.gameObject;

        if (layer == 0)
            SetLayersValue();

        controller.layer = layer;
        controller.GetComponent<SpriteRenderer>().sortingLayerName  = sortLayer;

        _UpdateLayers(controller);
    }
}
