using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Animator _animator;
    private int _layer;
    private string _sortingLayer;

    void Start(){
        _animator = transform.GetChild(0).GetComponent<Animator>();
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
        Destroy(transform.GetChild(0).GetComponent<BoxCollider2D>());
    }

    public void DesactivateDestructibleCollider(){
        Destroy(transform.GetChild(1).GetComponent<BoxCollider2D>());
    }

    public void DesactivateCarrierCollider(){
        if (_IsCarrier())
            Destroy(transform.GetChild(2).GetComponent<BoxCollider2D>());
    }

    /* ************************************************ */
    /* Drop Collectible */
    /* ************************************************ */
    public void DropCollectible(){
        if (_IsDroppingObject())
            transform.GetChild(3).GetComponent<DropCollectible>().DropCollectibleInField();
    }

    /* ************************************************ */
    /* Predicates */
    /* ************************************************ */
    private bool _IsCarrier(){
        if (transform.GetChild(2).name == "Tag_Carrier")
            return true;

        return false;
    }

    private bool _IsDroppingObject(){
        if (transform.GetChild(3).name == "Tag_DropObject")
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
    /* Sprite renderer */
    /* ************************************************ */
    public void ActiveSpriteRenderer(){
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    /* ************************************************ */
    /* Layer */
    /* ************************************************ */
    public string GetSortingLayerPlayerName(){
        string myLayer;

        myLayer = "player_" + SortingLayer.Split('_')[1];
        return myLayer;
    }
}
