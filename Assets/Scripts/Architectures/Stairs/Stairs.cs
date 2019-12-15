using UnityEngine;

abstract public class Stairs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
            return;

        GameObject player = other.gameObject;

        StairsInteraction(player);
    }

    protected abstract void StairsInteraction(GameObject player);
}
