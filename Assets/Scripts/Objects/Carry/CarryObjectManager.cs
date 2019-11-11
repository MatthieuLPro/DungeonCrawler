using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* type of elements to carry: */
/*
    - Pot
    - Bush
*/

public class CarryObjectManager : MonoBehaviour
{
    [Header("CarryObject Settings")]
    [SerializeField]
    private int _item = 0;
    [SerializeField]
    private bool hideTrigger = false;

    [SerializeField]
    private GameObject _myPrefab = null;
    
    private bool _open;

    /* Item list variable: */
    /* 
        0       = Random between(0 - 3)
        1       = Green Ruby
        2       = Heart
        3       = Mana
        4       = Blue Ruby 
        5       = Red Ruby 
        10      = key
        autre   = Rien
    */

    /* ************************************************ */
    /* Main Functions */
    /* ************************************************ */
    private void Start()
    {
        _open   = false;
        if (_item == 0)
            _item = Random.Range(0, 4);
    }

    /* ************************************************ */
    /* Functions */
    /* ************************************************ */

    public void CarryObject(GameObject player)
    {
        Vector3 ObjectCoord     = player.transform.position + new Vector3(0, 0.1f, 0);
        GameObject newPrefab    = Instantiate(_myPrefab, ObjectCoord, Quaternion.identity);
        
        newPrefab.GetComponent<HoldThrowObject>().SetCarrier(player.transform.Find("ActionTest").gameObject);
        newPrefab.transform.parent = player.transform.Find("ActionTest");
        Destroy(gameObject);
    }

    public void GenerateCollectible()
    {
        GameObject collectible = null;

        switch(_item)
        {
            case 1:
                collectible = Resources.Load("Prefabs/Collectible/CollectibleHealLife") as GameObject;
                break;
            case 2:
                collectible = Resources.Load("Prefabs/Collectible/CollectibleHealMana") as GameObject;
                break;
            case 3:
                collectible = Resources.Load("Prefabs/Collectible/ruby_green") as GameObject;
                break;
            case 4:
                collectible = Resources.Load("Prefabs/Collectible/ruby_blue") as GameObject;
                break;
            case 5:
                collectible = Resources.Load("Prefabs/Collectible/ruby_red") as GameObject;
                break;
            case 10:
                collectible = Resources.Load("Prefabs/Collectible/small_key") as GameObject;
                break;
            default:
                return;
        }
        Instantiate(collectible, transform.position, Quaternion.identity);
    }
}
