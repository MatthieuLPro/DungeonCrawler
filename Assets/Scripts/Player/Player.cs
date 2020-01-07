using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player parameters")]
    public int healthInit = 5;
    public int manaInit = 34;
    public int keys = 0;
    public bool bigKey = false;

    public void GetLife(int heal){
        transform.Find("UI").Find("HeartsHealthUI").GetComponent<HeartsHealthUI>().heartsHealthSystem.Heal(heal);
    }

    public void LooseLife(int damage){
        transform.Find("UI").Find("HeartsHealthUI").GetComponent<HeartsHealthUI>().heartsHealthSystem.Damage(damage);
    }

    public void GetMana(int heal){
        transform.Find("UI").Find("ManaUI").GetComponent<ManaUI>().manaSystem.ChangeMana(heal);
        //ManaUI.manaSystemStatic.ChangeMana(heal);
    }

    public void LooseMana(int damage){
        transform.Find("UI").Find("ManaUI").GetComponent<ManaUI>().manaSystem.ChangeMana(damage * -1);
        //ManaUI.manaSystemStatic.ChangeMana(damage * -1);
    }

    public void IsDead(){
        Destroy(GetComponent<PlayerController>());
    }

    public void GetSmallKey(){
        keys += 1;
    }

    public void LooseSmallKey(){
        keys -= 1;
    }
    
    public void GetBigKey(){
        bigKey = true;
    }

    // Predicate
    public bool HasSmallKey()
    {
        if (keys <= 0)
            return (false);
        return (true);
    }

    public bool HasBigKey()
    {
        if (bigKey == false)
            return (false);
        return (true);
    }
}
