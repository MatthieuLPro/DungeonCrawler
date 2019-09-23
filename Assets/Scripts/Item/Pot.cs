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
    // private int _item;
    private bool _open;

    private void Start(){
        _open   = false;
    }

    public void OpenThePot()
    {
        if (_open == true)
            return;

        _open = true;
        GetComponent<SpriteRenderer>().sprite = _spriteEmpty;
        while(gameObject.GetComponent<BoxCollider2D>() != null)
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        //foreach (var box in gameObject.GetComponents<BoxCollider2D>())
        //    Destroy(box);
    }

    public void GetThePot()
    {
        Instantiate(_myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

}