using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour
{
    [SerializeField]
    private Sprite _spriteEmpty = null;

    [SerializeField]
    private GameObject _myPrefab = null;

    // _item => 0 = Rien / 1 = Ruby / 2 = Coeur / 3 = Mana
    private int _item;
    private bool _open;

    private void Start(){
        _open   = false;
        _item   = Random.Range(0, 4);
    }

    public void OpenTheObject()
    {
        if (_open == true)
            return;

        _open = true;
        if (GetComponent<Animator>() != null)
            Destroy(GetComponent<Animator>());
        GetComponent<SpriteRenderer>().sprite = _spriteEmpty;
        GenerateCollectible();
        foreach (var box in gameObject.GetComponents<BoxCollider2D>())
            Destroy(box);
    }

    public void GetTheObject(GameObject player)
    {
        Vector3 ObjectCoord = player.transform.position + new Vector3(0, 0.1f, 0);
        GameObject newPrefab = Instantiate(_myPrefab, ObjectCoord, Quaternion.identity);
        newPrefab.GetComponent<CarryItem>().SetCarrier(player);
    }

    private void GenerateCollectible()
    {
        GameObject collectible = null;

        if (_item == 2 )
            collectible = Resources.Load("Prefabs/CollectibleHealLife") as GameObject;
        else if (_item == 3)
            collectible = Resources.Load("Prefabs/CollectibleHealMana") as GameObject;
        else
            return;

        Instantiate(collectible, transform.position, Quaternion.identity);
    }
}
