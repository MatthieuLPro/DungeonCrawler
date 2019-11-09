using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private PolygonCollider2D[] _collidersAttacks;
    private Animator _anime;
    private int _currentColliderIndex;

    void Start(){
        _currentColliderIndex = 0;
        _anime                = GetComponent<Animator>();
    }

    // Set Collider for attack Animation
    public void SetColliderForSprite(int spriteNum)
    {
        _collidersAttacks[_currentColliderIndex].enabled    = false;
        _currentColliderIndex                               = spriteNum;
        _collidersAttacks[_currentColliderIndex].enabled    = true;
    }

    public void SetColliderToNull()
    {
        for (var i = 0; i < _collidersAttacks.Length; i++)
            _collidersAttacks[i].enabled    = false;
    }

    private void SetColliderToAnimation()
    {
        float   x               = _anime.GetFloat("DirectionX");
        float   y               = _anime.GetFloat("DirectionY");
        string  attackDirection = "";

        _currentColliderIndex = 0;
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
}
