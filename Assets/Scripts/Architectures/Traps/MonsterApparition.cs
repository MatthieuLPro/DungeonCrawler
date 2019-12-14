using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApparition : MonoBehaviour
{
    [Header("Monster List")]
    [SerializeField]
    private GameObject[] _monsters;

    [Header("Trap settings")]
    [SerializeField]
    private bool _appearOneByOne;

    private bool _coIsWorking;

    void Start(){
        _coIsWorking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            if (_appearOneByOne)
                MonsterOneByOne();
            else
                MonsterOnce();
        }

        if (!_coIsWorking)
            Destroy(gameObject);
    }

    /* ************************************************ */
    /* Apparitions functions */
    /* ************************************************ */
    private void MonsterOnce()
    {
        for(var i = 0; i < _monsters.Length; i++)
            ShowMonster(_monsters[i]);
    }

    private void MonsterOneByOne(){
        StartCoroutine(OneByOneCo());
    }

    private void ShowMonster(GameObject monster)
    {
        monster.transform.GetChild(0).gameObject.SetActive(true);
        monster.transform.GetChild(1).gameObject.SetActive(true);
        monster.GetComponent<SpriteRenderer>().enabled = true;
        monster.GetComponent<BoxCollider2D>().enabled = true;
    }
  
    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    /* Monsters appear one by one */  
    private IEnumerator OneByOneCo()
    {
        _coIsWorking = true;
            
        for(var i = 0; i < _monsters.Length; i++)
        {
            ShowMonster(_monsters[i]);
            yield return new WaitForSeconds(1);
        }

        _coIsWorking = false;
        Destroy(gameObject);
    }
}
