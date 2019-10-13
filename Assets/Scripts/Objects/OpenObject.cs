using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenObject : MonoBehaviour
{
    [Header("Treasure Settings")]
    [SerializeField]
    private Sprite _open = null;
    [SerializeField]
    private int loot = 0;
    [SerializeField]
    private int _openMethod = 0;
    
    [Header("Loot Sprites")]
    [SerializeField]
    private Sprite _rubyGreen = null;
    [SerializeField]
    private Sprite _rubyBlue = null;
    [SerializeField]
    private Sprite _rubyRed = null;
    [SerializeField]
    private Sprite _rubyFift = null;
    [SerializeField]
    private Sprite _rubyHund = null;
    [SerializeField]
    private Sprite _rubyThreeHund = null;
    [SerializeField]
    private Sprite _smallKey = null;
    [SerializeField]
    private Sprite _bigKey = null;

    private SpriteRenderer _spriteRend;
    private GameObject lootObject;

    /*
        Loot values:
            - 1 : Ruby green
            - 2 : Ruby blue
            - 3 : Ruby red
            - 4 : Bomb 1
            - 5 : Bomb 3
            - 6 : Bomb 5
            - 7 : Arrow 5
            - 8 : Arrow 10
            - 9 : Arrow 15
            - 50 : Ruby 50
            - 51 : Ruby 100
            - 52 : Ruby 300
            - 100 : Small key
            - 101 : Grand key
        If loot = 0 => Random value between [1;6]
    */

    /*
        _openMethod values:
            - 0 : Can open
            - 1 : Normal key
            - 2 : Big Key
    */

    private void Start(){
        _spriteRend = GetComponent<SpriteRenderer>();
        if (loot == 0)
            loot = Random.Range(1,7);
    }

    public void OpenTheObject(GameObject opener){
        if (_openMethod == 2  && !opener.GetComponent<Player>().HasBigKey())
            return;
        else if (_openMethod == 1  && !opener.GetComponent<Player>().HasKey())
            return;

        _spriteRend.sprite = _open;
        foreach(BoxCollider2D box in gameObject.GetComponents<BoxCollider2D>())
        {
            if (box.isTrigger == true)
                Destroy(box);
        }
        GenerateLoot();
        StartCoroutine(ShowLoot(opener));
    }

    private IEnumerator ShowLoot(GameObject opener)
    {
        opener.GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(1f);

        opener.GetComponent<PlayerController>().enabled = true;
        Destroy(lootObject);
        GetLoot(opener);
    }

    private void GenerateLoot()
    {
        GameObject myPrefab;

        myPrefab = Resources.Load("Prefabs/Loot") as GameObject;
        lootObject = Instantiate(myPrefab, GetComponent<Transform>().position, Quaternion.identity);
        lootObject.GetComponent<Transform>().position += new Vector3(0, 0, -1);
        lootObject.GetComponent<SpriteRenderer>().sprite = LootSprite();
    }

    private Sprite LootSprite()
    {
        Sprite newSprite;

        switch(loot)
        {
            case 1:
                newSprite = _rubyGreen;
                break;
            case 2:
                newSprite = _rubyBlue;
                break;
            case 3:
                newSprite = _rubyRed;
                break;
            case 50:
                newSprite = _rubyFift;
                break;
            case 51:
                newSprite = _rubyHund;
                break;
            case 52:
                newSprite = _rubyThreeHund;
                break;
            case 100:
                newSprite = _smallKey;
                break;
            case 101:
                newSprite = _bigKey;
                break;
            default:
                newSprite = _rubyGreen;
                break;
        }
        return newSprite;
    }

    private void GetLoot(GameObject opener)
    {
        switch(loot)
        {
            case 1:
                opener.GetComponent<Player>().GetRuby(1);
                break;
            case 2:
                opener.GetComponent<Player>().GetRuby(5);
                break;
            case 3:
                opener.GetComponent<Player>().GetRuby(10);
                break;
            case 50:
                opener.GetComponent<Player>().GetRuby(50);
                break;
            case 51:
                opener.GetComponent<Player>().GetRuby(100);
                break;
            case 52:
                opener.GetComponent<Player>().GetRuby(300);
                break;
            default:
                opener.GetComponent<Player>().GetRuby(1);
                break;
        }
    }
}
