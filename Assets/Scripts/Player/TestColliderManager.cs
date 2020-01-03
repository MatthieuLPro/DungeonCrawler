using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColliderManager : MonoBehaviour
{
    private PolygonCollider2D[] _collidersAttacks;
    private BoxCollider2D[]     _collidersInteractionFronts;
    private Animator _anime;
    private int _currentColliderIndex;

    void Start()
    {
        _currentColliderIndex       = 0;
        _anime                      = GetComponent<Animator>();
        _collidersInteractionFronts = gameObject.transform.Find("Interaction").transform.Find("Front").GetComponents<BoxCollider2D>();
    }

    /* ************************************************ */
    /* Object detection in front Animation */
    /* ************************************************ */
    // Set Collider for object detection in front
    public void SetFrontColliderForSprite()
    {   
        int     boxIndex        = 0;
        float   x               = _anime.GetFloat("DirectionX");
        float   y               = _anime.GetFloat("DirectionY");

        if (x == 0)
            if (y > 0)
                boxIndex = 0;
            else
                boxIndex = 2;
        else if (x > 0)
            if (y < 0)
                boxIndex = 2;
            else
                boxIndex = 1;
        else
            if (y < 0)
                boxIndex = 2;
            else
                boxIndex = 3;


        if (_collidersInteractionFronts[boxIndex].enabled == true)
            return;

        for(var i = 0; i < 4; i++)
            _collidersInteractionFronts[i].enabled = false;

        _collidersInteractionFronts[boxIndex].enabled = true;
    }

    /* ************************************************ */
    /* Attack Animation */
    /* ************************************************ */
    // Set Collider for attack Animation
    public void SetAttackColliderForSprite(int spriteNum)
    {
        if (spriteNum >= _collidersAttacks.Length)
            return;

        _collidersAttacks[_currentColliderIndex].enabled    = false;
        _currentColliderIndex                               = spriteNum;
        _collidersAttacks[_currentColliderIndex].enabled    = true;
    }

    private void SetAttackColliderToAnimation()
    {
        float   x               = _anime.GetFloat("DirectionX");
        float   y               = _anime.GetFloat("DirectionY");
        string  attackDirection = "";

        SetAttackColliderToNull();

        if (x == 0)
            if (y > 0)
                attackDirection = "AttackTopTest";
            else
                attackDirection = "AttackBotTest";
        else if (x > 0)
            if (y < 0)
                attackDirection = "AttackBotTest";
            else
                attackDirection = "AttackRightTest";
        else
            if (y < 0)
                attackDirection = "AttackBotTest";
            else
                attackDirection = "AttackLeftTest";

        _collidersAttacks = gameObject.transform.Find("ActionTest").transform.Find(attackDirection).GetComponents<PolygonCollider2D>();
    }

    public void SetAttackColliderToNull()
    {
        string[] directions = new string[4] {"AttackTopTest", "AttackRightTest", "AttackBotTest", "AttackLeftTest"};

        _currentColliderIndex = 0;
        for (var i = 0; i < directions.Length; i++)
        {
            PolygonCollider2D[] colliders = gameObject.transform.Find("ActionTest").transform.Find(directions[i]).GetComponents<PolygonCollider2D>();
            for (var j = 0; j < colliders.Length; j++)
                colliders[j].enabled    = false;
        }
    }
}
