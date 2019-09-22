using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5;
    public int mana = 5;
    public int gold = 0;

    public void GetLife(int heal){
        HeartsHealthUI.heartsHealthSystemStatic.Heal(heal);
    }

    public void LooseLife(int damage){
        HeartsHealthUI.heartsHealthSystemStatic.Damage(damage);
    }

    public void GetMana(int heal){
        ManaUI.manaSystemStatic.ChangeMana(heal);
    }

    public void LooseMana(int damage){
        ManaUI.manaSystemStatic.ChangeMana(damage * -1);
    }
}