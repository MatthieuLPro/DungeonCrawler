using System.Collections;
using UnityEngine;

public class Rubies : MonoBehaviour
{
    public int          value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        ResultPlayer resultPlayer = other.transform.parent.Find("Result").GetComponent<ResultPlayer>();
        
        resultPlayer.GetRuby(value);
        Destroy(gameObject);
    }

    private IEnumerator GetRubyCo()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        
        Destroy(gameObject);
    }
}
