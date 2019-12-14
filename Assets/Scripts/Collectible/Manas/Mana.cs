using UnityEngine;

abstract public class Mana : MonoBehaviour
{
    public int heal = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.GetMana(heal);
            Destroy(gameObject);
        }     
    }
}
