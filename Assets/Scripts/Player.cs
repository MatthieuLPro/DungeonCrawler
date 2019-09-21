using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5;
    public int mana = 5;
    public int gold = 0;

    public void DamageMana(Vector3 knockBackDir, float knockBackDistance, int damage)
    {
        transform.position += knockBackDir * knockBackDistance;
        ManaUI.manaSystemStatic.ChangeMana(damage * -1);
    }

    public void Heal(int heal){
        HeartsHealthUI.heartsHealthSystemStatic.Heal(heal);
    }
    
    public void HealMana(int heal){
        ManaUI.manaSystemStatic.ChangeMana(heal);
    }
}
