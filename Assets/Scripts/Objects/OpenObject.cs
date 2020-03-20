using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
    [Header("Treasure Settings")]
    [SerializeField]
    private Sprite _openTop = null;
    [SerializeField]
    private Sprite _openBot = null;
    [SerializeField]
    private int _loot = 0;
    [SerializeField]
    private int _openMethod = 0;

    private GameObject _lootObject;

    /*
        Loot values:
            - 1 : Ruby green
            - 2 : Ruby blue
            - 3 : Ruby red
            - 50 : Ruby 50
            - 51 : Ruby 100
            - 52 : Ruby 300
            - 100 : Small key
            - 101 : Grand key
        If loot = 0 => Random value between [1;6]
    */

    /*
        _openMethod values:
            - 0 : Do not need key
            - 1 : Need small key
            - 2 : Need big key
    */

    private void Start(){
        if (Loot == 0)
            Loot = Random.Range(1,7);
    }

    public void TryToOpen(GameObject opener) {
        if (!_CanOpenObject(opener)) 
            return;
        _ChangeSpriteToOpen();
        _OpenTheObject(opener);
    }

    bool _CanOpenObject(GameObject opener) {
        Player player = opener.GetComponent<Player>();
        if (_openMethod == 2  && !player.HasBigKey())
            return false;
        else if (_openMethod == 1  && !player.HasSmallKey())
            return false;
        
        return true;
    } 

    void _OpenTheObject(GameObject opener){
        foreach(BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            if (box.isTrigger == true)
                Destroy(box);
        }
        _GeneratePrefabLoot();
        StartCoroutine(_LootAnimationCo(opener));
    }

    void _ChangeSpriteToOpen() {
        GetComponent<SpriteRenderer>().sprite = _openBot;
        transform.parent.GetComponent<SpriteRenderer>().sprite = _openTop;
    }

    void _GeneratePrefabLoot()
    {
        GameObject myPrefab;

        myPrefab        = Resources.Load("Prefabs/Collectible/Loot") as GameObject;
        _lootObject     = Instantiate(myPrefab, GetComponent<Transform>().position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        _lootObject.GetComponent<SpriteRenderer>().sprite = _GenerateLootSprite();
    }

    Sprite _GenerateLootSprite()
    {
        Sprite newSprite;
        Object[] sprites = Resources.LoadAll("Sprites/Objects/collectibles");

        switch(_loot)
        {
            case 1:
                newSprite = (Sprite)sprites[14];
                break;
            case 2:
                newSprite = (Sprite)sprites[11]; Resources.Load<Sprite>("Sprites/Objects/ruby_blue1");
                break;
            case 3:
                newSprite = (Sprite)sprites[17]; Resources.Load<Sprite>("Sprites/Objects/ruby_red1");
                break;
            case 50:
                newSprite = (Sprite)sprites[8]; Resources.Load<Sprite>("Sprites/Objects/ruby50");
                break;
            case 51:
                newSprite = (Sprite)sprites[9]; Resources.Load<Sprite>("Sprites/Objects/ruby100");
                break;
            case 52:
                newSprite = (Sprite)sprites[10]; Resources.Load<Sprite>("Sprites/Objects/ruby300");
                break;
            case 100:
                newSprite = (Sprite)sprites[5]; Resources.Load<Sprite>("Sprites/Objects/key_small");
                break;
            case 101:
                newSprite = (Sprite)sprites[4]; Resources.Load<Sprite>("Sprites/Objects/key_big");
                break;
            default:
                newSprite = (Sprite)sprites[14];
                break;
        }
        return newSprite;
    }

    IEnumerator _LootAnimationCo(GameObject opener)
    {
        opener.transform.GetChild(0).Find("Movement").GetComponent<Movement>().enabled = false;
        yield return new WaitForSeconds(1f);

        opener.transform.GetChild(0).Find("Movement").GetComponent<Movement>().enabled = true;
        Destroy(_lootObject);
        _GetReward(opener);
    }

    void _GetReward(GameObject opener)
    {
        ResultPlayer resultPlayer   = opener.GetComponent<ResultPlayer>();
        Player player               = opener.GetComponent<Player>();

        switch(Loot)
        {
            case 1:
                resultPlayer.GetRuby(1);
                break;
            case 2:
                resultPlayer.GetRuby(5);
                break;
            case 3:
                resultPlayer.GetRuby(10);
                break;
            case 50:
                resultPlayer.GetRuby(50);
                break;
            case 51:
                resultPlayer.GetRuby(100);
                break;
            case 52:
                resultPlayer.GetRuby(300);
                break;
            case 100:
                player.GetSmallKey();
                break;
            case 101:
                player.GetBigKey();
                break;
            default:
                resultPlayer.GetRuby(1);
                break;
        }
    }

    // Getter & Setter
    public int Loot {
        get { return _loot; }
        set { _loot = value; }
    }
}
