using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Animator _animator;
    private int _layer;
    private string _sortingLayer;

    void Start(){
        _animator = gameObject.GetComponent<Animator>();
        Layer = gameObject.layer;
        SortingLayer = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
    }

    /* ************************************************ */
    /* Getter & setter */
    /* ************************************************ */
    public int Layer { 
        get { return _layer; }
        set { _layer = value; }
    }

    public string SortingLayer { 
        get { return _sortingLayer; }
        set { _sortingLayer = value; }
    }

    /* ************************************************ */
    /* Desactivate Box Collider */
    /* ************************************************ */
    public void DesactivateMovementCollider(){
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }

    public void DesactivateDestructibleCollider(){
        Destroy(transform.GetChild(0).GetComponent<BoxCollider2D>());
    }

    public void DesactivateCarrierCollider(){
        if (_IsCarrier())
            Destroy(transform.GetChild(1).GetComponent<BoxCollider2D>());
    }

    /* ************************************************ */
    /* Drop Collectible */
    /* ************************************************ */
    public void DropCollectible(){
        if (_IsDroppingObject())
            transform.GetChild(2).GetComponent<DropCollectible>().DropCollectibleInField();
    }

    /* ************************************************ */
    /* Predicates */
    /* ************************************************ */
    private bool _IsCarrier(){
        if (transform.GetChild(1).name == "Tag_Carrier")
            return true;

        return false;
    }

    private bool _IsDroppingObject(){
        if (transform.GetChild(2).name == "Tag_DropObject")
            return true;

        return false;
    }

    /* ************************************************ */
    /* Animation */
    /* ************************************************ */
    public void AnimationDestroy(){
        _animator.SetBool("destroying", true);
    }

    /* ************************************************ */
    /* Layer */
    /* ************************************************ */
    public void UpdateSortingLayer(){
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        sprite.sortingLayerName = GetSortingLayerPlayerName();
        sprite.sortingOrder = -2;
    }

    public string GetSortingLayerPlayerName(){
        string myLayer;

        myLayer = "player_" + SortingLayer.Split('_')[1];
        return myLayer;
    }
}
