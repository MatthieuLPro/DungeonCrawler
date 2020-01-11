using UnityEngine;

abstract public class Hearts : MonoBehaviour
{
    [HideInInspector]
    public int heal = 0;

    private AudioSource _audio = null;

    private void Start(){
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player player = other.transform.parent.GetComponent<Player>();
        if(player != null)
        {
            player.GetLife(heal);
            _audio.Play();
            Destroy(gameObject);
        }     
    }
}
