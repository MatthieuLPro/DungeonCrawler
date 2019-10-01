using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player parameters")]
    public int healthInit = 5;
    public int manaInit = 34;
    public int rubyInit = 0;
    public int keys = 0;
    public bool bigKey = false;

    public void GetRuby(int rubyAmount){
        RubyUI.rubySystemStatic.ChangeRuby(rubyAmount);
    }

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

    public void IsDead(){
        Destroy(GetComponent<PlayerController>());
    }

    public void GetSmallKey(){
        keys += 1;
    }
    
    public void GetBigKey(){
        bigKey = true;
    }

    public bool HasKey()
    {
        if (keys <= 0)
            return (false);

        keys--;
        return (true);
    }

    public bool HasBigKey()
    {
        if (bigKey == false)
            return (false);
        return (true);
    }
}
