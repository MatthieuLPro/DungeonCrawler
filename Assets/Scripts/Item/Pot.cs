using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
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

    public void OpenThePot()
    {
        if (_open == true)
            return;

        _open = true;
        GetComponent<SpriteRenderer>().sprite = _spriteEmpty;
        GenerateItem();
        foreach (var box in gameObject.GetComponents<BoxCollider2D>())
            Destroy(box);
    }

    public void GetThePot(GameObject player)
    {
        Vector3 potCoord = player.transform.position + new Vector3(0, 0.1f, 0);
        GameObject newPrefab = Instantiate(_myPrefab, potCoord, Quaternion.identity);
        newPrefab.GetComponent<CarryItem>().SetCarrier(player);
    }

    private void GenerateItem()
    {
        GameObject objectToGenerate = null;

        if (_item == 2 )
            objectToGenerate = Resources.Load("Prefabs/ObjectHealLife") as GameObject;
        else if (_item == 3)
            objectToGenerate = Resources.Load("Prefabs/ObjectHealMana") as GameObject;
        else
            return;

        Instantiate(objectToGenerate, transform.position, Quaternion.identity);
    }

}