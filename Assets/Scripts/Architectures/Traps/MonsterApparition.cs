using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApparition : MonoBehaviour
{
    [Header("Monster List")]
    [SerializeField]
    private GameObject[] _monsters = null;

    [Header("Trap settings")]
    [SerializeField]
    private bool _appearOneByOne = false;

    private bool _coIsWorking;

    void Start(){
        _coIsWorking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (_appearOneByOne)
            MonsterOneByOne();
        else
            MonsterAll();

        if (!_coIsWorking)
            Destroy(gameObject);
    }

    /* ************************************************ */
    /* Apparitions functions */
    /* ************************************************ */
    private void MonsterAll()
    {
        for(var i = 0; i < _monsters.Length; i++)
            ShowMonster(_monsters[i]);
    }

    private void MonsterOneByOne(){
        StartCoroutine(OneByOneCo());
    }

    private void ShowMonster(GameObject monster){
        monster.gameObject.SetActive(true);
    }
  
    /* ************************************************ */
    /* Coroutines */
    /* ************************************************ */

    /* Monsters appear one by one effect */  
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
