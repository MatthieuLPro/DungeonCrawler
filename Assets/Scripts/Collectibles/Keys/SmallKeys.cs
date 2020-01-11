using UnityEngine;

public class SmallKeys : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        GetComponent<AudioSource>().Play();
        other.transform.parent.GetComponent<Player>().GetSmallKey();
        Destroy(gameObject);
    }
}
