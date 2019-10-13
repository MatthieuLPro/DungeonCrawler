using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    [Header("CarryObject Settings")]
    [SerializeField]
    private int _item = 0;
    [SerializeField]
    private bool hideTrigger = false;

    [SerializeField]
    private Sprite _spriteEmpty = null;

    [SerializeField]
    private GameObject _myPrefab = null;

    /* _item => 
        0  = Rien
        1  = Green Ruby
        2  = Heart
        3  = Mana
        4  = Blue Ruby 
        5  = Red Ruby 
        10 = key
    */
    private bool _open;

    private void Start()
    {
        _open   = false;
        if (_item == 0)
            _item = Random.Range(0, 4);
    }

    public void RaiseTheObject()
    {
        if (_open == true)
            return;

        _open = true;
        if (GetComponent<Animator>() != null)
            Destroy(GetComponent<Animator>());
        GetComponent<SpriteRenderer>().sprite = _spriteEmpty;
        foreach (var box in gameObject.GetComponents<BoxCollider2D>())
            Destroy(box);
        if (hideTrigger)
            Destroy(gameObject);
        else
            GenerateCollectible();
    }

    public void GetTheObject(GameObject player)
    {
        Vector3 ObjectCoord = player.transform.position + new Vector3(0, 0.1f, 0);
        GameObject newPrefab = Instantiate(_myPrefab, ObjectCoord, Quaternion.identity);
        newPrefab.GetComponent<HoldThrowObject>().SetCarrier(player);
    }

    private void GenerateCollectible()
    {
        GameObject collectible = null;

        if (_item == 1)
            collectible = Resources.Load("Prefabs/Collectible/CollectibleHealLife") as GameObject;
        else if (_item == 2 )
            collectible = Resources.Load("Prefabs/Collectible/CollectibleHealLife") as GameObject;
        else if (_item == 3)
            collectible = Resources.Load("Prefabs/Collectible/ruby_green") as GameObject;
        else if (_item == 4)
            collectible = Resources.Load("Prefabs/Collectible/ruby_blue") as GameObject;
        else if (_item == 5)
            collectible = Resources.Load("Prefabs/Collectible/ruby_red") as GameObject;
        else if (_item == 10)
            collectible = Resources.Load("Prefabs/Collectible/small_key") as GameObject;
        else
            return;

        Instantiate(collectible, transform.position, Quaternion.identity);
    }
}
