using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private PolygonCollider2D[] _collidersAttacks;
    private Animator _anime;
    private int _currentColliderIndex;

    void Start()
    {
        _currentColliderIndex = 0;
        _anime                = GetComponent<Animator>();
    }

    // Set Collider for attack Animation
    public void SetColliderForSprite(int spriteNum)
    {
        if (spriteNum >= _collidersAttacks.Length)
            return;

        _collidersAttacks[_currentColliderIndex].enabled    = false;
        _currentColliderIndex                               = spriteNum;
        _collidersAttacks[_currentColliderIndex].enabled    = true;
    }

    private void SetColliderToAnimation()
    {
        float   x               = _anime.GetFloat("DirectionX");
        float   y               = _anime.GetFloat("DirectionY");
        string  attackDirection = "";

        SetColliderToNull();

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

    public void SetColliderToNull()
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
