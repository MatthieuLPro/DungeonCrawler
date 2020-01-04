using UnityEngine;

abstract public class Manas : MonoBehaviour
{
    [HideInInspector]
    public int heal = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.parent.GetComponent<Player>();
        if(player != null)
        {
            player.GetMana(heal);
            Destroy(gameObject);
        }     
    }
}
