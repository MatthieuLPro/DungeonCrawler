using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Item list variable: */
    /* 
        0       = Random between(0 - 3)
        1       = Green Ruby
        2       = Heart
        3       = Mana 
        10      = key
        other   = Nothing
    */

public class DropCollectible : MonoBehaviour
{
    [Header("Drop collectible Settings")]
    [SerializeField]
    private int _item = 0;
    private GameObject _myPrefab = null;

    private ObjectManager _manager;

    void Start(){
        if (_item == 0) 
            _item = Random.Range(0, 3);
        _myPrefab = _GetCollectiblePrefab();

        _manager = transform.parent.GetComponent<ObjectManager>();
    }

    public void DropCollectibleInField(){
        _InstantiateCollectible();
    }

    /* ************************************************ */
    /* Generate prefab */
    /* ************************************************ */
    private GameObject _GetCollectiblePrefab()
    {
        GameObject collectible = null;

        switch(_item)
        {
            case 1:
                collectible = Resources.Load("Prefabs/Collectible/ruby_green") as GameObject;
                break;
            case 2:
                collectible = Resources.Load("Prefabs/Collectible/small_heart") as GameObject;
                break;
            case 3:
                collectible = Resources.Load("Prefabs/Collectible/small_mana") as GameObject;
                break;
            case 10:
                collectible = Resources.Load("Prefabs/Collectible/small_key") as GameObject;
                break;
            default:
                collectible = null;
                break;
        }

        return collectible;
    }

    private void _InstantiateCollectible(){
        if (_myPrefab == null) return;

        GameObject collectible  = Instantiate(_myPrefab, transform.position, Quaternion.identity);
        SpriteRenderer sprite   = collectible.GetComponent<SpriteRenderer>();

        collectible.layer = _manager.Layer;
        sprite.sortingLayerName = _manager.GetSortingLayerPlayerName();
        sprite.sortingOrder = -1;
    }
}
