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

    private GameObject _lootObject = null;

    private GameObject _info = null;

    private GameObject _soundManager = null;

    /*
        _loot values:
            - 1 : Ruby green
            - 2 : Ruby blue
            - 3 : Ruby red
            - 50 : Ruby 50
            - 51 : Ruby 100
            - 52 : Ruby 300
            - 100 : Small key
            - 101 : Grand key
        If _loot = 0 => Random value between [1;6]
    */

    /*
        _openMethod values:
            - 1 : Do not need key
            - 2 : Need small key
            - 3 : Need big key
        If _openMethod = 0 => Random value between [1;3]
    */

    private void Start(){
        Object[] sprites = Resources.LoadAll("Sprites/Objects/treasures/treasure_info");

        if (Loot == 0)
            Loot = Random.Range(1,7);
        if (OpenMethod == 0)
            OpenMethod = Random.Range(1,4);
            
        _soundManager   = transform.parent.transform.Find("Sound").gameObject;
        _info           = transform.parent.transform.Find("InfoTreasure").gameObject;
        if (OpenMethod == 3)
            _info.GetComponent<SpriteRenderer>().sprite = (Sprite)sprites[1];

        _info.active = false;

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PlayerInteractionFront")) {
            ShowOpenInfo();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        HideOpenInfo();
    }

    public void TryToOpen(GameObject opener) {
        if (!_CanOpenObject(opener)) { 
            _PlayAudioClip();
            return;
        }
        if (OpenMethod == 3)
            _UpdateAudioClip("openBig");
        else
            _UpdateAudioClip("openSmall");
        _PlayAudioClip();
        _ChangeSpriteToOpen();
        _OpenTheObject(opener);
    }

    public void ShowOpenInfo() {
        if (OpenMethod == 2 || OpenMethod == 3)
            _info.active = true;
    }

    public void HideOpenInfo() {
        if (OpenMethod == 2 || OpenMethod == 3)
            _info.active = false;
    }

    bool _CanOpenObject(GameObject opener) {
        Player player = opener.GetComponent<Player>();
        if (OpenMethod == 3  && !player.HasBigKey())
            return false;
        else if (OpenMethod == 2  && !player.HasSmallKey())
            return false;
        
        Destroy(_info);
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
        GetComponent<SpriteRenderer>().sprite                   = _openBot;
        transform.parent.GetComponent<SpriteRenderer>().sprite  = _openTop;
    }

    void _GeneratePrefabLoot()
    {
        GameObject myPrefab;

        myPrefab        = Resources.Load("Prefabs/Collectible/Loot") as GameObject;
        LootObject     = Instantiate(myPrefab, GetComponent<Transform>().position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        LootObject.GetComponent<SpriteRenderer>().sprite = _GenerateLootSprite();
    }

    Sprite _GenerateLootSprite()
    {
        Sprite newSprite;
        Object[] sprites = Resources.LoadAll("Sprites/Objects/collectibles");

        switch(Loot)
        {
            case 1:
                newSprite = (Sprite)sprites[14];
                break;
            case 2:
                newSprite = (Sprite)sprites[11];
                break;
            case 3:
                newSprite = (Sprite)sprites[17];
                break;
            case 50:
                newSprite = (Sprite)sprites[8];
                break;
            case 51:
                newSprite = (Sprite)sprites[9];
                break;
            case 52:
                newSprite = (Sprite)sprites[10];
                break;
            case 100:
                newSprite = (Sprite)sprites[5];
                break;
            case 101:
                newSprite = (Sprite)sprites[4];
                break;
            default:
                newSprite = (Sprite)sprites[14];
                break;
        }
        return newSprite;
    }

    IEnumerator _LootAnimationCo(GameObject opener)
    {
        opener.transform.GetChild(0).Find("Movement").GetComponent<Movement>().blockMovement = true;
        yield return new WaitForSeconds(1f);

        opener.transform.GetChild(0).Find("Movement").GetComponent<Movement>().blockMovement = false;
        Destroy(LootObject);
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

    public GameObject LootObject {
        get { return _lootObject; }
        set { _lootObject = value; }
    }

    public int OpenMethod {
        get { return _openMethod; }
        set { _openMethod = value; }
    }

    /* ************************************************ */
    /* Sound */
    /* ************************************************ */
    private void _UpdateAudioClip(string newAudioClip){
        _soundManager.GetComponent<AudioManager>().CallAudio(newAudioClip);
    }

    private void _PlayAudioClip(){
        _soundManager.GetComponent<AudioManager>().PlayAudio();
    }
}
