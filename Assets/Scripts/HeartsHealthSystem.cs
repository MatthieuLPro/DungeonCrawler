using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsHealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private List<Heart> heartList;

    public HeartsHealthSystem(int heartAmount)
    {
        heartList = new List<Heart>();
        for(int i = 0; i < heartAmount; i++)
        {
            Heart heart = new Heart(2);
            heartList.Add(heart);
        }
    }

    public List<Heart> GetHeartList()
    {
        return heartList;
    }

    public void Damage(int damage)
    {
        int lastHeartIndex = FindLastHeartWithLife();
        while(damage > 0)
        {
            if (heartList[lastHeartIndex].GetValue() == 0)
                lastHeartIndex--;
            if (lastHeartIndex < 0)
                break;
            heartList[lastHeartIndex].GetSingularDamage();
            damage--;
        }
        if (OnDamaged != null)
            OnDamaged(this, EventArgs.Empty);
    }

    public void Heal(int heal)
    {
        int lastHeartIndex = FindLastHeartWithLife();

        while(heal > 0)
        {
            if (heartList[lastHeartIndex].GetValue() == 2)
                lastHeartIndex++;
            if (lastHeartIndex >= heartList.Count)
                break;
            heartList[lastHeartIndex].GetSingularHeal();
            heal--;
        }
        if (OnHealed != null)
            OnHealed(this, EventArgs.Empty);
    }

    private int FindLastHeartWithLife()
    {
        for(int i = 0; i < heartList.Count; i++)
        {
            if (i == (heartList.Count - 1) || heartList[i + 1].GetValue() == 0)
                return (i);
        }
        return (0);
    }

    public class Heart
    {
        private int value;

        public Heart(int value){
            this.value = value;
        }

        public int GetValue(){
            return this.value;
        }

        public void SetValue(int value){
            this.value = value;
        }

        public void GetSingularDamage(){
            this.value--;
        }

        public void GetSingularHeal(){
            this.value++;
        }
    }
}
